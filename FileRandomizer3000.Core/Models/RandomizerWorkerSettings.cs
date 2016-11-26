using FileRandomizer3000.Core.Enums;

namespace FileRandomizer3000.Core.Models
{
    public class RandomizerWorkerSettings
    {
        public virtual double SizeLimitBytes { get; set; }
        public virtual int FilesNumberLimit { get; set; }
        public virtual int FilesNumberPerFolderLimit { get; set; }
        public virtual int FoldersNumberLimit { get; set; }
        public virtual string[] PathsFrom { get; set; }
        public virtual string PathTo { get; set; }
        public virtual bool DeleteFromTargetFolder { get; set; }
        public virtual bool FindOnlyUniqueFiles { get; set; }
        public virtual bool OnDuplicateDoNotCopy { get; set; }
        public virtual LimitType SelectedLimit { get; set; }
    }
}