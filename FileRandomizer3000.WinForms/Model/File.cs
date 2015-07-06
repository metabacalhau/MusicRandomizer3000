using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace BusinessAccessLayer
{
    public class FileInFolder
    {
        public static FileInFolder Create(FileInfo file)
        {
            return new FileInFolder()
            {
                FileName = file.Name,
                FilePath = file.FullName,
                FileSize = file.Length,
                Extension = file.Extension
            };
        }

        public string FilePath { get; set; }
        public string FileName { get; set; }
        public double FileSize { get; set; }
        public string Extension { get; set; }
    }

    public class FileInFolderComparer : IEqualityComparer<FileInFolder>
    {
        public bool Equals(FileInFolder x, FileInFolder y)
        {
            if (Object.ReferenceEquals(x, y))
            {
                return true;
            }

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
            {
                return false;
            }

            return x.FileName == y.FileName && x.FileSize == y.FileSize;
        }

        public int GetHashCode(FileInFolder obj)
        {
            return obj.FileName.GetHashCode() ^ obj.FileSize.GetHashCode();
        }
    }
}
