using FileRandomizer3000.Core.Enums;
using FileRandomizer3000.Core.Services.Interfaces;
using System.Collections.Generic;

namespace FileRandomizer3000.Core.Models
{
    public class CopyWorkerSettings
    {
        public virtual UniqueCharsPosition CharsPositionOnDuplicate { get; set; }
        public virtual UniqueCharsPosition CharsPositionOnCopy { get; set; }
        public virtual IList<AppFile> FilesToCopy { get; set; }
        public virtual string PathTo { get; set; }
        public virtual int FoldersNumber { get; set; }
        public virtual int FilesPerFolderNumber { get; set; }
        public virtual bool OnDuplicateDoNotCopy { get; set; }
        public virtual bool OnDuplicateOverwrite { get; set; }
        public virtual bool OnDuplicateAddPrefix { get; set; }
        public virtual bool OnDuplicateAddSuffix { get; set; }
        public virtual LimitType SelectedLimit { get; set; }
        public virtual SortOrder SelectedSortOrder { get; set; }
    }
}