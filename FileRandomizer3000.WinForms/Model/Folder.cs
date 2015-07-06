using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BusinessAccessLayer
{
    public class Folder
    {
        public static Folder Create(string path, bool hasSubFolders)
        {
            return new Folder()
            {
                FolderLabel = new DirectoryInfo(path).Name,
                FolderPath = path,
                HasSubFolders = hasSubFolders
            };
        }

        public IEnumerable<Folder> Folders;
        public bool HasSubFolders { get; set; }
        public string FolderPath { get; set; }
        public string FolderLabel { get; set; }
        public long FolderSize { get; set; }
        public DriveInfo Drive { get; set; }
    }
}
