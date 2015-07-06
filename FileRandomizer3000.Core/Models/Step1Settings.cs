namespace FileRandomizer3000.Core.Models
{
    public class Step1Settings : IStepSetting
    {
        public string PathFrom { get; set; }
        public int? SelectedFilesLimitID { get; set; }
        public int? SelectedSizeID { get; set; }
        public int FilesNumber { get; set; }
        public double SizeLimit { get; set; }
        public bool FindOnlyUniqueFiles { get; set; }
        public bool UseRecursiveSearch { get; set; }
        public bool OrganizeFiles { get; set; }
        public int? FoldersNumber { get; set; }
        public int? FilesInFolderNumber { get; set; }
        public bool SaveSettings { get; set; }
    }
}