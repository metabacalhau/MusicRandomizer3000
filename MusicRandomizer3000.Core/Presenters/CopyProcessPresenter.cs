using MusicRandomizer3000.Core.Infrastructure.Interfaces;
using MusicRandomizer3000.Core.Models;
using MusicRandomizer3000.Core.Services.Interfaces;
using MusicRandomizer3000.Core.ViewModels;
using MusicRandomizer3000.Core.Views;
using System;
using System.Collections.Generic;

namespace MusicRandomizer3000.Core.Presenters
{
    public class CopyProcessPresenter : BaseWizardPresenter<ICopyProcessView, List<AppFile>>
    {
        private readonly ICopyWorker _copyWorker;
        private readonly IProcessWrapper _processWrapper;
        private CopyProcessViewModel _model;

        public CopyProcessPresenter(IApplicationController controller, ICopyProcessView view, CopyProcessViewModel model, ICopyWorker copyWorker, IProcessWrapper processWrapper)
            : base(controller, view)
        {
            if (model == null) throw new ArgumentNullException("model");
            if (copyWorker == null) throw new ArgumentNullException("copyWorker");
            if (processWrapper == null) throw new ArgumentNullException("processWrapper");

            _model = model;
            _copyWorker = copyWorker;
            _processWrapper = processWrapper;
        }

        public override void Run(List<AppFile> filesToCopy)
        {
            _model.Settings.FilesToCopy = filesToCopy;

            View.OnBeforeViewShown += OnBeforeViewShown;
            View.OnCopyStopped += OnCopyStopped;
            View.OnShowCopiedFiles += OnShowCopiedFiles;
            View.OnPreviousStepClick += OnPreviousStepClick;

            View.Initialize(_model);

            View.SetActiveView();
        }

        private void OnBeforeViewShown()
        {
            CopyWorkerParameters parameters = new CopyWorkerParameters
            {
                Settings = _model.Settings,
                OnStarted = OnStarted,
                OnFinished = OnFinished,
                OnFailed = OnFailed,
                OnCancelled = OnCancelled,
                OnFileChanged = OnFileChanged,
                OnProgressChanged = OnProgressChanged
            };

            _copyWorker.Run(parameters);

            View.StartCopy();
        }

        private void OnCopyStopped()
        {
            _copyWorker.Cancel();
            View.StopCopy();
        }

        public void OnFinished()
        {
            View.CopyFinished();
        }

        public void OnFailed(string errorMessage)
        {
            View.AlertErrorMessage(errorMessage);
        }

        public void OnStarted()
        {
            View.CopyStarted();
        }

        public void OnProgressChanged(int progressPercentage)
        {
            View.ReportProgress(progressPercentage);
        }

        public void OnFileChanged(AppFile file)
        {
            _model.ProgressInfoText = file.FileNameFull;
        }

        public void OnCancelled()
        {
            View.CopyAborted();
        }

        public void OnShowCopiedFiles()
        {
            _processWrapper.Start(_model.Settings.PathTo);
        }

        public void OnPreviousStepClick()
        {
            Controller.RunSingleton<Step3Presenter>();
        }
    }
}