using FileRandomizer3000.Core.Models;
using System.Collections.Generic;

namespace FileRandomizer3000.Core.Services.Interfaces
{
    public interface IFolderService
    {
        bool IsAccessible(string folderPath);
        IEnumerable<AppFolder> GetFolders(string folderPath);
    }
}