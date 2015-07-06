using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessAccessLayer;
using System.IO;
using System.ComponentModel;

namespace RandomFiles.Model
{
    public static class Extension
    {
        public static string GetSize(this IEnumerable<FileInFolder> FileList)
        {
            double size = 0;
            foreach (FileInFolder file in FileList)
            {
                size += file.FileSize;
            }

            return Repository.GetSize(size);
        }

        public static IEnumerable<FileInFolder> GetRandomFiles(this IEnumerable<FileInFolder> FilesSource, Information Info)
        {
            if (FilesSource.Count() > 0)
            {
                var Random = new Random();

                var FilesToCopy = new List<FileInFolder>();

                while (FilesToCopy.Sum(file => file.FileSize) < Info.DriveAvailableFreeSpace)
                {
                    double FileListSize = FilesToCopy.Sum(file => file.FileSize);

                    var RandomFile = FilesSource.ToList()[Random.Next(0, FilesSource.Count())];

                    if (FileListSize + RandomFile.FileSize < Info.DriveAvailableFreeSpace)
                    {
                        FileListSize += RandomFile.FileSize;
                        FilesToCopy.Add(RandomFile);
                        yield return RandomFile;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public static IEnumerable<FileInFolder> GetRandomFiles(this IEnumerable<FileInFolder> FilesSource, Information Info, IEnumerable<FileInFolder> FilesInTargetFolder)
        {
            if (FilesSource.Count() > 0)
            {
                var Random = new Random();

                var FilesToCopy = new List<FileInFolder>();

                var SourceFilesWithoutTargetFiles = FilesSource.Where(file => !FilesInTargetFolder.Contains(file, new FileInFolderComparer())).ToList();

                while (FilesToCopy.Sum(file => file.FileSize) < Info.DriveAvailableFreeSpace)
                {
                    double FileListSize = FilesToCopy.Sum(file => file.FileSize);

                    if (SourceFilesWithoutTargetFiles.Count > 0)
                    {
                        var RandomFile = SourceFilesWithoutTargetFiles.ToList()[Random.Next(0, SourceFilesWithoutTargetFiles.Count())];

                        if (FileListSize + RandomFile.FileSize < Info.DriveAvailableFreeSpace)
                        {
                            FileListSize += RandomFile.FileSize;
                            FilesToCopy.Add(RandomFile);
                            yield return RandomFile;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public static IEnumerable<FileInFolder> GetUniqueRandomFiles(this IEnumerable<FileInFolder> FilesSource, Information Info)
        {
            if (FilesSource.Count() > 0)
            {
                var Random = new Random();

                var FilesToCopy = new List<FileInFolder>();

                while (FilesToCopy.Sum(file => file.FileSize) < Info.DriveAvailableFreeSpace)
                {
                    double FilesToCopySize = FilesToCopy.Sum(file => file.FileSize);

                    var UniqueFiles = FilesSource.Where(file => !FilesToCopy.Contains(file, new FileInFolderComparer())).ToList();

                    if (UniqueFiles.Count > 0)
                    {
                        var RandomFile = UniqueFiles[Random.Next(0, UniqueFiles.Count())];

                        if (FilesToCopySize + RandomFile.FileSize < Info.DriveAvailableFreeSpace && FilesToCopy.Count() <= FilesSource.Count())
                        {
                            FilesToCopySize += RandomFile.FileSize;
                            FilesToCopy.Add(RandomFile);
                            yield return RandomFile;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public static IEnumerable<FileInFolder> GetUniqueRandomFiles(this IEnumerable<FileInFolder> FilesSource, Information Info, IEnumerable<FileInFolder> FilesInTargetFolder)
        {
            if (FilesSource.Count() > 0)
            {
                var Random = new Random();

                var FilesToCopy = new List<FileInFolder>();

                var UniqueFiles = FilesSource.Where(file => !FilesToCopy.Contains(file, new FileInFolderComparer()) && !FilesInTargetFolder.Contains(file, new FileInFolderComparer())).ToList();

                while (FilesToCopy.Sum(file => file.FileSize) < Info.DriveAvailableFreeSpace && FilesToCopy.Count != UniqueFiles.Count)
                {
                    double FilesToCopySize = FilesToCopy.Sum(file => file.FileSize);

                    if (UniqueFiles.Count > 0)
                    {
                        var RandomFile = UniqueFiles[Random.Next(0, UniqueFiles.Count())];

                        if (!FilesToCopy.Contains(RandomFile, new FileInFolderComparer()))
                        {
                            if (FilesToCopySize + RandomFile.FileSize < Info.DriveAvailableFreeSpace && FilesToCopy.Count() <= FilesSource.Count())
                            {
                                FilesToCopySize += RandomFile.FileSize;
                                FilesToCopy.Add(RandomFile);
                                yield return RandomFile;
                            }
                            else
                            {
                                break;
                            }
                        }
                        //else
                        //{
                        //    break;
                        //}
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public static IEnumerable<FileInFolder> CopyTo(this IEnumerable<FileInFolder> FilesSource, string PathTo)
        {
            FileInFolder CurrentFile;

            foreach (FileInFolder file in FilesSource.OrderBy(item => item.FileName))
            {
                try
                {
                    new FileInfo(file.FilePath).CopyTo(Path.Combine(PathTo, file.FileName), false);
                    CurrentFile = file;
                }
                catch
                {
                    string name = Path.GetFileNameWithoutExtension(file.FilePath);
                    new FileInfo(file.FilePath).CopyTo(Path.Combine(PathTo, name + " [" + DateTime.Now.GetHashCode() + "]" + file.Extension), false);
                    CurrentFile = file;
                }

                yield return CurrentFile;
            }
        }

        public static void InvokeEx<T>(this T @this, Action<T> action) where T : ISynchronizeInvoke
        {
            if (@this.InvokeRequired)
            {
                @this.Invoke(action, new object[] { @this });
            }
            else
            {
                action(@this);
            }
        }
    }
}
