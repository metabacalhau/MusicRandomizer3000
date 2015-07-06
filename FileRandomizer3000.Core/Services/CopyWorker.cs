using FileRandomizer3000.Core.Services.Interfaces;
using FileRandomizer3000.Core.Models;
using System;
using System.Linq;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;
using FileRandomizer3000.Core.Enums;

namespace FileRandomizer3000.Core.Services
{
    public class CopyWorker : ICopyWorker
    {
        private readonly IFileSystem _fileSystem;
        private readonly IFileHelper _fileHelper;
        private readonly IContext _uiContext;
        private readonly IBackgroundWorker _backgroundWorker;
        private readonly IUniqueCharsGenerator _charsGenerator;

        public CopyWorker(IFileSystem fileSystem, IFileHelper fileHelper, IContext uiContext, IBackgroundWorker backgroundWorker, IUniqueCharsGenerator charsGenerator)
        {
            if (fileSystem == null) throw new ArgumentNullException("fileSystem");
            if (fileHelper == null) throw new ArgumentNullException("fileHelper");
            if (uiContext == null) throw new ArgumentNullException("uiContext");
            if (backgroundWorker == null) throw new ArgumentNullException("backgroundWorker");
            if (charsGenerator == null) throw new ArgumentNullException("charsGenerator");

            _fileSystem = fileSystem;
            _fileHelper = fileHelper;
            _uiContext = uiContext;
            _backgroundWorker = backgroundWorker;
            _charsGenerator = charsGenerator;
        }

        public void Run(CopyWorkerParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");
            if (parameters.Settings == null) throw new ArgumentNullException("settings");
            if (parameters.OnStarted == null) throw new ArgumentNullException("onStarted");
            if (parameters.OnFileChanged == null) throw new ArgumentNullException("onFileChanged");
            if (parameters.OnProgressChanged == null) throw new ArgumentNullException("onProgressChanged");
            if (parameters.OnFinished == null) throw new ArgumentNullException("onFinished");
            if (parameters.OnFailed == null) throw new ArgumentNullException("onFailed");
            if (parameters.OnCancelled == null) throw new ArgumentNullException("onCancelled");

            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;

            _backgroundWorker.OnProgressChanged += (sender, e) =>
            {
                _uiContext.Post(x => parameters.OnProgressChanged(e.ProgressPercentage), null);
                _uiContext.Post(x => parameters.OnFileChanged((AppFile)e.UserState), null);
            };

            _backgroundWorker.OnDoWork += (sender, e) =>
            {
                _uiContext.Post(x => parameters.OnStarted(), null);

                if (!IsCancellationPending(e))
                {
                    int index = 1;
                    int filesCount = parameters.Settings.FilesToCopy.Count();

                    RenameAndSortFiles(parameters.Settings);

                    if (parameters.Settings.SelectedLimit == LimitType.FilesNumberPerFolder)
                    {
                        int currentFolderIndex = 0;
                        string pathTo = "";
                        int processedFilesCount = 0;

                        foreach (AppFile file in parameters.Settings.FilesToCopy)
                        {
                            if (currentFolderIndex == 0 || processedFilesCount == parameters.Settings.FilesPerFolderNumber)
                            {
                                ++currentFolderIndex;
                                processedFilesCount = 0;
                                pathTo = string.Format("{0}/CD{1:00}", parameters.Settings.PathTo, currentFolderIndex);
                                _fileSystem.CreateDirectory(pathTo);
                            }

                            string filePath = string.Format(CultureInfo.CurrentCulture, "{0}/{1}", pathTo, file.FileNameFull);

                            // if exists skip file copy and move to the next file
                            if (_fileSystem.FileExists(filePath))
                            {
                                if (parameters.Settings.OnDuplicateDoNotCopy)
                                {
                                    ReportProgress(file, ref index, filesCount);
                                    continue;
                                }
                                else if (parameters.Settings.OnDuplicateOverwrite)
                                {
                                    _fileSystem.DeleteFile(filePath);
                                }
                                else if (parameters.Settings.OnDuplicateAddPrefix || parameters.Settings.OnDuplicateAddSuffix)
                                {
                                    file.FileNameFull = _fileHelper.GenerateUniqueFileName(file, _charsGenerator, parameters.Settings.CharsPositionOnDuplicate);
                                }
                            }

                            if (IsCancellationPending(e)) return;

                            if (currentFolderIndex > parameters.Settings.FoldersNumber)
                            {
                                break;
                            }

                            _fileSystem.CopyTo(file.FilePath, string.Format(CultureInfo.CurrentCulture, "{0}/{1}", pathTo, file.FileNameFull));

                            ReportProgress(file, ref index, filesCount);

                            ++processedFilesCount;
                        }
                    }
                    else
                    {
                        foreach (AppFile file in parameters.Settings.FilesToCopy)
                        {
                            string filePath = string.Format(CultureInfo.CurrentCulture, "{0}/{1}", parameters.Settings.PathTo, file.FileNameFull);

                            // if exists skip file copy and move to the next file
                            if (_fileSystem.FileExists(filePath))
                            {
                                if (parameters.Settings.OnDuplicateDoNotCopy)
                                {
                                    ReportProgress(file, ref index, filesCount);
                                    continue;
                                }
                                else if (parameters.Settings.OnDuplicateOverwrite)
                                {
                                    _fileSystem.DeleteFile(filePath);
                                }
                                else if (parameters.Settings.OnDuplicateAddPrefix || parameters.Settings.OnDuplicateAddSuffix)
                                {
                                    file.FileNameFull = _fileHelper.GenerateUniqueFileName(file, _charsGenerator, parameters.Settings.CharsPositionOnDuplicate);
                                }
                            }

                            if (IsCancellationPending(e)) return;

                            _fileSystem.CopyTo(file.FilePath, string.Format(CultureInfo.CurrentCulture, "{0}/{1}", parameters.Settings.PathTo, file.FileNameFull));

                            ReportProgress(file, ref index, filesCount);
                        }
                    }
                }

                e.Result = true;
            };

            _backgroundWorker.OnRunWorkerCompleted += (sender, e) =>
            {
                if (e.Error != null)
                {
                    _uiContext.Post(x => parameters.OnFailed(e.Error.Message), null);
                }
                else if (e.Cancelled)
                {
                    _uiContext.Post(x => parameters.OnCancelled(), null);
                }
                else
                {
                    _uiContext.Post(x => parameters.OnFinished(), null);
                }
            };

            if (_backgroundWorker.IsBusy)
            {
                _backgroundWorker.CancelAsync();
            }

            _backgroundWorker.RunWorkerAsync();
        }

        public void Cancel()
        {
            _backgroundWorker.CancelAsync();
        }

        private bool IsCancellationPending(DoWorkEventArgs e)
        {
            if (_backgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ReportProgress(AppFile file, ref int index, int filesCount)
        {
            int percentageDone = (int)(100.0 / filesCount * index);
            _backgroundWorker.ReportProgress(percentageDone, file);
            ++index;
        }

        private void RenameAndSortFiles(CopyWorkerSettings settings)
        {
            foreach (AppFile file in settings.FilesToCopy)
            {
                file.FileNameFull = _fileHelper.GenerateUniqueFileName(file, _charsGenerator, settings.CharsPositionOnCopy);
            }

            if (settings.SelectedSortOrder == SortOrder.Ascending)
            {
                settings.FilesToCopy = settings.FilesToCopy.OrderBy(x => x.FileNameFull).ToList();
            }
            else if (settings.SelectedSortOrder == SortOrder.Descending)
            {
                settings.FilesToCopy = settings.FilesToCopy.OrderByDescending(x => x.FileNameFull).ToList();
            }
        }
    }
}
