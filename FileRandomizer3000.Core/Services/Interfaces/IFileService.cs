using FileRandomizer3000.Core.Enums;
using FileRandomizer3000.Core.Models;
using System.Collections.Generic;

namespace FileRandomizer3000.Core.Services.Interfaces
{
    public interface IFileService
    {
        IEnumerable<AppFile> GetFiles(string folderPath);
        IEnumerable<AppFile> GetFilesByMaxFileSize(bool findOnlyUniqueFiles, IEnumerable<AppFile> files, IEnumerable<AppFile> existingFiles, double maxSizeLimit);
        IEnumerable<AppFile> GetFilesByFilesNumber(bool findOnlyUniqueFiles, IEnumerable<AppFile> files, IEnumerable<AppFile> existingFiles, int filesNumberLimit);
        void DeleteFiles(IEnumerable<AppFile> files);
        void DeleteFile(AppFile file);
        double GetTotalSize(IEnumerable<AppFile> files);
        string GetTotalSizeFormatted(IEnumerable<AppFile> files);
    }
}