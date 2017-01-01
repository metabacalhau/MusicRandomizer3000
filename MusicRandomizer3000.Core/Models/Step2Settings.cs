namespace MusicRandomizer3000.Core.Models
{
    public class Step2Settings : IStepSetting
    {
        public string PathTo { get; set; }
        public int OnCopyActionID { get; set; }
        public bool SaveSettings { get; set; }
    }
}