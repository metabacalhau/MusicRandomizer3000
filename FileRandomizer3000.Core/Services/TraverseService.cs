using FileRandomizer3000.Core.Services.Interfaces;
using FileRandomizer3000.Core.Models;
using System;
using System.Collections.Generic;

namespace FileRandomizer3000.Core.Services
{
    public class TraverseService : ITraverseService
    {
        private readonly IFileService _fileService = null;
        private readonly IFolderService _folderService = null;

        public TraverseService(IFileService fileService, IFolderService folderService)
        {
            if (fileService == null) throw new ArgumentNullException("fileService");
            if (folderService == null) throw new ArgumentNullException("folderService");

            _fileService = fileService;
            _folderService = folderService;
        }

        public virtual IEnumerable<AppFile> TraverseFolder(string folderPath)
        {
            if (_folderService.IsAccessible(folderPath))
            {
                foreach (AppFile file in _fileService.GetFiles(folderPath))
                {
                    yield return file;
                }

                foreach (AppFolder folder in _folderService.GetFolders(folderPath))
                {
                    if (_folderService.IsAccessible(folder.FolderPath))
                    {
                        foreach (AppFile file in TraverseFolder(folder.FolderPath))
                        {
                            yield return file;
                        }
                    }
                }
            }
        }
    }
}
