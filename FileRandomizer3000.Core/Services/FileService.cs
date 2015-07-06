using FileRandomizer3000.Core.Enums;
using FileRandomizer3000.Core.Models;
using FileRandomizer3000.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileRandomizer3000.Core.Services
{
    public class FileService : IFileService
    {
        private readonly IFileSystem _fileSystem;
        private readonly IFileHelper _fileHelper;
        private readonly IEqualityComparer<AppFile> _fileComparer;

        public FileService(IFileSystem fileSystem, IFileHelper fileHelper, IEqualityComparer<AppFile> fileComparer)
        {
            if (fileSystem == null) throw new ArgumentNullException("fileSystem");
            if (fileHelper == null) throw new ArgumentNullException("fileHelper");
            if (fileComparer == null) throw new ArgumentNullException("fileComparer");

            _fileSystem = fileSystem;
            _fileHelper = fileHelper;
            _fileComparer = fileComparer;
        }

        public IEnumerable<AppFile> GetFiles(string folderPath)
        {
            foreach (string filePath in _fileSystem.GetFiles(folderPath))
            {
                AppFile file = _fileSystem.CreateAppFile(filePath);

                yield return file;
            }
        }

        private bool CanProcess(IEnumerable<AppFile> files, double maxSizeLimit)
        {
            if (files == null) throw new ArgumentNullException("files");
            if (maxSizeLimit <= 0) throw new ArgumentOutOfRangeException("maxSizeLimit");

            if (files.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DeleteFile(AppFile file)
        {
            if (file != null)
            {
                _fileSystem.DeleteFile(file.FilePath);
            }
        }

        public void DeleteFiles(IEnumerable<AppFile> files)
        {
            if (files != null)
            {
                foreach (AppFile file in files)
                {
                    DeleteFile(file);
                }
            }
        }

        public double GetTotalSize(IEnumerable<AppFile> files)
        {
            double result = 0;

            if (files != null)
            {
                result = files.Sum(o => o.FileSize);
            }

            return result;
        }

        public string GetTotalSizeFormatted(IEnumerable<AppFile> files)
        {
            return _fileHelper.FileSizeFormatter(GetTotalSize(files));
        }

        public IEnumerable<AppFile> GetFilesByMaxFileSize(bool findOnlyUniqueFiles, IEnumerable<AppFile> files, IEnumerable<AppFile> existingFiles, double maxSizeLimit)
        {
            if (findOnlyUniqueFiles)
            {
                if (CanProcess(files, maxSizeLimit))
                {
                    HashSet<AppFile> filesToCopy = new HashSet<AppFile>(_fileComparer);
                    HashSet<AppFile> filesFiltered = new HashSet<AppFile>(_fileComparer);

                    if (existingFiles != null && existingFiles.Count() > 0)
                    {
                        foreach (AppFile file in files.Where(file => !existingFiles.Contains(file, _fileComparer)))
                        {
                            filesFiltered.Add(file);
                        }
                    }
                    else
                    {
                        foreach (AppFile file in files)
                        {
                            filesFiltered.Add(file);
                        }
                    }

                    if (filesFiltered.Count > 0)
                    {
                        double totalFileSize = 0;

                        while (totalFileSize < maxSizeLimit && filesToCopy.Count != filesFiltered.Count)
                        {
                            AppFile randomFile = _fileHelper.GetRandomFile(filesFiltered);

                            if (randomFile == null)
                            {
                                break;
                            }

                            double estimatedTotalFileSize = totalFileSize + randomFile.FileSize;

                            if (estimatedTotalFileSize < maxSizeLimit && filesToCopy.Count + 1 <= files.Count())
                            {
                                if (filesToCopy.Add(randomFile))
                                {
                                    totalFileSize += randomFile.FileSize;

                                    yield return randomFile;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                if (CanProcess(files, maxSizeLimit))
                {
                    List<AppFile> filesToCopy = new List<AppFile>();
                    List<AppFile> filesFiltered = new List<AppFile>();

                    double totalFileSize = 0;

                    // select only non existing files (distinct)
                    if (existingFiles != null && existingFiles.Count() > 0)
                    {
                        filesFiltered = files.Where(file => !existingFiles.Contains(file, _fileComparer)).ToList();
                    }

                    while (totalFileSize < maxSizeLimit)
                    {
                        AppFile randomFile = null;

                        if (filesFiltered.Count > 0)
                        {
                            randomFile = _fileHelper.GetRandomFile(filesFiltered);
                        }
                        else
                        {
                            randomFile = _fileHelper.GetRandomFile(files);
                        }

                        if (randomFile == null)
                        {
                            break;
                        }

                        double estimatedTotalFileSize = totalFileSize + randomFile.FileSize;

                        if (estimatedTotalFileSize < maxSizeLimit)
                        {
                            totalFileSize += randomFile.FileSize;

                            filesToCopy.Add(randomFile);

                            yield return randomFile;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        public IEnumerable<AppFile> GetFilesByFilesNumber(bool findOnlyUniqueFiles, IEnumerable<AppFile> files, IEnumerable<AppFile> existingFiles, int filesNumberLimit)
        {
            if (findOnlyUniqueFiles)
            {
                HashSet<AppFile> filesToCopy = new HashSet<AppFile>(_fileComparer);
                HashSet<AppFile> filesFiltered = new HashSet<AppFile>(_fileComparer);

                if (existingFiles != null && existingFiles.Count() > 0)
                {
                    foreach (AppFile file in files.Where(file => !existingFiles.Contains(file, _fileComparer)))
                    {
                        filesFiltered.Add(file);
                    }
                }
                else
                {
                    foreach (AppFile file in files)
                    {
                        filesFiltered.Add(file);
                    }
                }

                if (filesFiltered.Count > 0)
                {
                    int filesCount = 0;

                    while (filesCount < filesNumberLimit && filesToCopy.Count != filesFiltered.Count)
                    {
                        AppFile randomFile = _fileHelper.GetRandomFile(filesFiltered);

                        if (randomFile == null)
                        {
                            break;
                        }

                        if (filesToCopy.Add(randomFile))
                        {
                            ++filesCount;

                            yield return randomFile;
                        }
                    }
                }
            }
            else
            {
                List<AppFile> filesFiltered = new List<AppFile>();

                // select only non existing files (distinct)
                if (existingFiles != null && existingFiles.Count() > 0)
                {
                    filesFiltered = files.Where(file => !existingFiles.Contains(file, _fileComparer)).ToList();
                }

                for (int i = 1; i <= filesNumberLimit; i++)
                {
                    AppFile randomFile = null;

                    if (filesFiltered.Count > 0)
                    {
                        randomFile = _fileHelper.GetRandomFile(filesFiltered);
                    }
                    else
                    {
                        randomFile = _fileHelper.GetRandomFile(files);
                    }

                    if (randomFile == null)
                    {
                        break;
                    }

                    yield return randomFile;
                }
            }
        }
    }
}
