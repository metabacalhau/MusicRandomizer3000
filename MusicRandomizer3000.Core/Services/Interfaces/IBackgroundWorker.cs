using System.ComponentModel;

namespace MusicRandomizer3000.Core.Services.Interfaces
{
    public interface IBackgroundWorker
    {
        event ProgressChangedEventHandler OnProgressChanged;
        event RunWorkerCompletedEventHandler OnRunWorkerCompleted;
        event DoWorkEventHandler OnDoWork;

        bool IsBusy { get; }
        bool CancellationPending { get; }
        bool WorkerReportsProgress { get; set; }
        bool WorkerSupportsCancellation { get; set; }

        void RunWorkerAsync();
        void RunWorkerAsync(object args);
        void CancelAsync();
        void ReportProgress(int percentProgress);
        void ReportProgress(int percentProgress, object argument);
    }
}