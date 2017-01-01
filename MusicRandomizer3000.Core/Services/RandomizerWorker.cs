using MusicRandomizer3000.Core.Services.Interfaces;
using MusicRandomizer3000.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;
using MusicRandomizer3000.Core.Enums;

namespace MusicRandomizer3000.Core.Services
{
    public class RandomizerWorker : IRandomizerWorker
    {
        private readonly IFileService _fileService;
        private readonly ITraverseService _traverseService;
        private readonly IContext _uiContext;
        private readonly IBackgroundWorker _backgroundWorker;

        public RandomizerWorker(IFileService fileService, ITraverseService traverseService, IContext uiContext, IBackgroundWorker backgroundWorker)
        {
            if (fileService == null) throw new ArgumentNullException("fileService");
            if (traverseService == null) throw new ArgumentNullException("traverseService");
            if (uiContext == null) throw new ArgumentNullException("uiContext");
            if (backgroundWorker == null) throw new ArgumentNullException("backgroundWorker");

            _fileService = fileService;
            _traverseService = traverseService;
            _uiContext = uiContext;
            _backgroundWorker = backgroundWorker;
        }

        public void Run(RandomizerWorkerSettings settings, Action<List<AppFile>> onFinished, Action<string> onFailed, Action onCancelled)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            if (onFinished == null) throw new ArgumentNullException("onFinished");
            if (onFailed == null) throw new ArgumentNullException("onFailed");
            if (onCancelled == null) throw new ArgumentNullException("onCancelled");

            _backgroundWorker.WorkerSupportsCancellation = true;

            _backgroundWorker.OnDoWork += (sender, e) =>
            {
                List<AppFile> filesFound = new List<AppFile>();
                List<AppFile> filesToCopy = new List<AppFile>();
                List<AppFile> filesExisting = new List<AppFile>();

                if (!IsCancellationPending(e))
                {
                    if (settings.DeleteFromTargetFolder)
                    {
                        foreach (AppFile info in _fileService.GetFiles(settings.PathTo))
                        {
                            if (IsCancellationPending(e)) return;
                            _fileService.DeleteFile(info);
                        }
                    }
                }

                if (!IsCancellationPending(e))
                {
                    if (settings.OnDuplicateDoNotCopy)
                    {
                        foreach (AppFile file in _fileService.GetFiles(settings.PathTo))
                        {
                            if (IsCancellationPending(e)) return;
                            filesExisting.Add(file);
                        }
                    }
                }

                if (!IsCancellationPending(e) && settings.PathsFrom != null)
                {
                    foreach (string path in settings.PathsFrom)
                    {
                        foreach (AppFile file in _traverseService.TraverseFolder(path))
                        {
                            if (IsCancellationPending(e)) return;
                            filesFound.Add(file);
                        }
                    }
                }

                if (!IsCancellationPending(e))
                {
                    if (settings.SelectedLimit == LimitType.FilesTotalSize)
                    {
                        foreach (AppFile file in _fileService.GetFilesByMaxFileSize(settings.FindOnlyUniqueFiles, filesFound, filesExisting, settings.SizeLimitBytes))
                        {
                            if (IsCancellationPending(e)) return;
                            filesToCopy.Add(file);
                        }
                    }
                    else if (settings.SelectedLimit == LimitType.FilesNumber || settings.SelectedLimit == LimitType.FilesNumberPerFolder)
                    {
                        int filesNumber = 0;

                        if (settings.SelectedLimit == LimitType.FilesNumber)
                        {
                            filesNumber = settings.FilesNumberLimit;
                        }
                        else if (settings.SelectedLimit == LimitType.FilesNumberPerFolder)
                        {
                            filesNumber = settings.FoldersNumberLimit * settings.FilesNumberPerFolderLimit;
                        }

                        foreach (AppFile file in _fileService.GetFilesByFilesNumber(settings.FindOnlyUniqueFiles, filesFound, filesExisting, filesNumber))
                        {
                            if (IsCancellationPending(e)) return;
                            filesToCopy.Add(file);
                        }
                    }
                }

                e.Result = filesToCopy;
            };

            _backgroundWorker.OnRunWorkerCompleted += (sender, e) =>
            {
                if (e.Error != null)
                {
                    _uiContext.Post(x => onFailed(e.Error.Message), null);
                }
                else if (e.Cancelled)
                {
                    _uiContext.Post(x => onCancelled(), null);
                }
                else
                {
                    _uiContext.Post(x => onFinished((List<AppFile>)e.Result), null);
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
    }
}