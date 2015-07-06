using FileRandomizer3000.Core.Services.Interfaces;
using System.ComponentModel;

namespace FileRandomizer3000.Core.Services
{
    public class BackgroundWorkerAsync : IBackgroundWorker
    {
        private readonly BackgroundWorker _worker = new BackgroundWorker();

        public event RunWorkerCompletedEventHandler OnRunWorkerCompleted
        {
            add { _worker.RunWorkerCompleted += value; }
            remove { _worker.RunWorkerCompleted -= value; }
        }

        public event DoWorkEventHandler OnDoWork
        {
            add { _worker.DoWork += value; }
            remove { _worker.DoWork -= value; }
        }

        public event ProgressChangedEventHandler OnProgressChanged
        {
            add { _worker.ProgressChanged += value; }
            remove { _worker.ProgressChanged -= value; }
        }

        public bool IsBusy
        {
            get { return _worker.IsBusy; }
        }

        public bool CancellationPending
        {
            get { return _worker.CancellationPending; }
        }

        public bool WorkerReportsProgress
        {
            get { return _worker.WorkerReportsProgress; }
            set { _worker.WorkerReportsProgress = value; }
        }

        public bool WorkerSupportsCancellation
        {
            get { return _worker.WorkerSupportsCancellation; }
            set { _worker.WorkerSupportsCancellation = value; }
        }

        public void RunWorkerAsync()
        {
            _worker.RunWorkerAsync();
        }

        public void RunWorkerAsync(object args)
        {
            _worker.RunWorkerAsync(args);
        }

        public void CancelAsync()
        {
            _worker.CancelAsync();
        }

        public void ReportProgress(int percentProgress)
        {
            _worker.ReportProgress(percentProgress);
        }

        public void ReportProgress(int percentProgress, object argument)
        {
            _worker.ReportProgress(percentProgress, argument);
        }
    }
}