using FileRandomizer3000.Core.Models;
using System.Collections.Generic;

namespace FileRandomizer3000.Core.Services.Interfaces
{
    public interface IFileSystem
    {
        void CopyTo(string sourcePath, string destinationPath);
        bool FolderExists(string folderPath);
        bool FileExists(string filePath, string folderPath);
        bool FileExists(string filePath);
        bool IsDriveRemovable(string drivePath);
        void DeleteFile(string filePath);
        IEnumerable<string> GetFiles(string folderPath);
        IEnumerable<string> GetFolders(string folderPath);
        AppFile CreateAppFile(string filePath);
        void CreateDirectory(string folderPath);
    }
}