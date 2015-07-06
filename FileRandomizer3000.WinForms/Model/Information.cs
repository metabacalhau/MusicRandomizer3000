using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessAccessLayer;

namespace RandomFiles.Model
{
    public class Information
    {
        public IEnumerable<FileInFolder> FilesInFolder { get; set; }
        public IEnumerable<FileInFolder> FilesToCopy { get; set; }
        public int FilesInFolderCount { get; set; }
        public int FilesToCopyCount { get; set; }
        public double ElapsedTime { get; set; }
        public double DriveAvailableFreeSpace { get; set; }
        public string PathFrom { get; set; }
        public string PathTo { get; set; }
    }
}
