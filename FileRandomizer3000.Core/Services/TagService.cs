using FileRandomizer3000.Core.Services.Interfaces;
using FileRandomizer3000.Core.Models;
using HundredMilesSoftware.UltraID3Lib;
using System;
using System.Collections.Generic;

namespace FileRandomizer3000.Core.Services
{
    public class TagService : ITagService
    {
        private readonly IFileSystem fileSystem;

        public TagService(IFileSystem FileSystem)
        {
            if (FileSystem == null) throw new ArgumentNullException("FileSystem");

            fileSystem = FileSystem;
        }

        public IEnumerable<AppFile> GetFiles(string FolderPath)
        {
            foreach (string FilePath in fileSystem.GetFiles(FolderPath))
            {
                UltraID3 ultra = new UltraID3();
                ultra.Read(FilePath);
            }

            throw new NotImplementedException();
        }
    }
}
