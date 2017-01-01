using MusicRandomizer3000.Core.Services.Interfaces;
using System;
using System.ComponentModel;

namespace MusicRandomizer3000.Core.Services
{
    public class BackgroundWorkerSync : IBackgroundWorker
    {
        public event RunWorkerCompletedEventHandler OnRunWorkerCompleted = delegate { };
        public event DoWorkEventHandler OnDoWork = delegate { };
        public event ProgressChangedEventHandler OnProgressChanged;

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            private set { isBusy = value; }
        }

        private bool cancellationPending;
        public bool CancellationPending
        {
            get { return cancellationPending; }
            private set { cancellationPending = value; }
        }

        public bool WorkerReportsProgress { get; set; }

        public bool WorkerSupportsCancellation { get; set; }

        public void RunWorkerAsync()
        {
            RunWorkerAsync(null);
        }

        public void RunWorkerAsync(object args)
        {
            IsBusy = true;
            CancellationPending = false;

            object result = null;
            Exception error = null;
            bool cancelled = false;

            try
            {
                DoWorkEventArgs e = new DoWorkEventArgs(args);

                if (OnDoWork != null)
                {
                    OnDoWork(this, e);
                }

                if (e.Cancel || CancellationPending)
                {
                    cancelled = true;
                }
                else
                {
                    result = e.Result;
                }
            }
            catch (Exception ex)
            {
                error = ex;
            }

            IsBusy = false;
            CancellationPending = false;

            if (OnRunWorkerCompleted != null)
            {
                OnRunWorkerCompleted(this, new RunWorkerCompletedEventArgs(result, error, cancelled));
            }
        }

        public void CancelAsync()
        {
            if (!WorkerSupportsCancellation) throw new InvalidOperationException("Worker doesn't support cancellation");

            CancellationPending = true;
        }

        public void ReportProgress(int percentProgress)
        {
            ReportProgress(percentProgress, null);
        }

        public void ReportProgress(int percentProgress, object argument)
        {
            if (!WorkerReportsProgress) throw new InvalidOperationException("Worker doesn't support progress report");

            if (OnProgressChanged != null)
            {
                ProgressChangedEventArgs args = new ProgressChangedEventArgs(percentProgress, argument);
                OnProgressChanged(this, args);
            }
        }
    }
}