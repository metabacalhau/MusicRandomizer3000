using System;

namespace FileRandomizer3000.Core.Models
{
    public class CopyWorkerParameters
    {
        public CopyWorkerSettings Settings { get; set; }
        public Action OnStarted { get; set; }
        public Action<AppFile> OnFileChanged { get; set; }
        public Action<int> OnProgressChanged { get; set; }
        public Action OnFinished { get; set; }
        public Action<string> OnFailed { get; set; }
        public Action OnCancelled { get; set; }
    }
}