namespace FileRandomizer3000.Core.Models
{
    public class Step3Settings : IStepSetting
    {
        public int OnDuplicateOptionID { get; set; }
        public int SortOptionID { get; set; }
        public bool ShowRandomizedFiles { get; set; }
        public bool SaveSettings { get; set; }
    }
}