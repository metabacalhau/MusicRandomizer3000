using System;

namespace MusicRandomizer3000.Core.Models
{
    public class AppFile
    {
        public string FileNameFull { get; set; }
        public string FileNameWithoutExtension { get; set; }
        public string FilePath { get; set; }
        public double FileSize { get; set; }
        public string FileExtension { get; set; }
    }
}