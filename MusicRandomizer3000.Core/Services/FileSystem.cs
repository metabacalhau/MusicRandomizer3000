using MusicRandomizer3000.Core.Models;
using MusicRandomizer3000.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace MusicRandomizer3000.Core.Services
{
    public class FileSystem : IFileSystem
    {
        private readonly IFileExtension _fileExtensions;

        public FileSystem(IFileExtension fileExtensions)
        {
            if (fileExtensions == null) throw new ArgumentNullException("fileExtensions");

            _fileExtensions = fileExtensions;
        }

        public bool FolderExists(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool FileExists(string filePath, string folderPath)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(string sourcePath, string destinationPath)
        {
            FileInfo fileInfo = new FileInfo(sourcePath).CopyTo(destinationPath, false);
        }

        public bool IsDriveRemovable(string drivePath)
        {
            return new DriveInfo(Path.GetPathRoot(drivePath)).DriveType == DriveType.Removable;
        }

        public void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        public IEnumerable<string> GetFolders(string folderPath)
        {
            if (FolderExists(folderPath))
            {
                foreach (string folder in Directory.GetDirectories(folderPath))
                {
                    yield return folder;
                }
            }
        }

        public IEnumerable<string> GetFiles(string folderPath)
        {
            if (FolderExists(folderPath))
            {
                foreach (string extension in _fileExtensions.AllowedExtensions)
                {
                    foreach (string file in Directory.GetFiles(folderPath, extension))
                    {
                        yield return file;
                    }
                }
            }
        }

        public bool FileExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public AppFile CreateAppFile(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            AppFile file = new AppFile();

            file.FileNameFull = fileInfo.Name;
            file.FileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            file.FilePath = fileInfo.FullName;
            file.FileSize = fileInfo.Length;
            file.FileExtension = fileInfo.Extension;

            return file;
        }

        public void CreateDirectory(string folderPath)
        {
            if (!FolderExists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }
    }
}