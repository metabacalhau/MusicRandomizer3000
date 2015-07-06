using FileRandomizer3000.Core.Services.Interfaces;
using FileRandomizer3000.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileRandomizer3000.Core.Services
{
    public class FolderService : IFolderService
    {
        public bool IsAccessible(string folderPath)
        {
            bool isAccessible = true;

            DirectoryInfo directory = new DirectoryInfo(folderPath);

            try
            {
                directory.GetDirectories().Any();
            }
            catch (DirectoryNotFoundException)
            {
                isAccessible = false;
            }

            return isAccessible;
        }

        public IEnumerable<AppFolder> GetFolders(string folderPath)
        {
            foreach (string path in Directory.GetDirectories(folderPath))
            {
                if (IsAccessible(path))
                {
                    DirectoryInfo info = new DirectoryInfo(path);

                    AppFolder folder = new AppFolder();
                    folder.FolderName = info.Name;
                    folder.FolderPath = info.FullName;

                    yield return folder;
                }
            }
        }
    }
}
