using FileRandomizer3000.Core.Views;
using FileRandomizer3000.Core.ViewModels;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace FileRandomizer3000.WinForms.Views
{
    public partial class CopyProcessView : UserControl, ICopyProcessView
    {
        private readonly IMainFormViewHost _viewHost;

        public CopyProcessView(IMainFormViewHost viewHost)
        {
            _viewHost = viewHost;
        }

        public event Action OnBeforeViewShown;

        public event Action OnCopyStopped;

        public event Action OnPreviousStepClick;

        public event Action OnShowCopiedFiles;

        public void Initialize(CopyProcessViewModel model)
        {
            if (model == null) throw new ArgumentNullException("model");

            Model = model;

            InitializeComponent();

            lblCurrentFile.DataBindings.Add("Text", Model, "ProgressInfoText", false, DataSourceUpdateMode.OnPropertyChanged);

            progressBar.DataBindings.Add("Value", Model, "ProgressPercentage", false, DataSourceUpdateMode.OnPropertyChanged);

            btnStopCopyProcess.Location = new Point(300, 159);
            btnStopCopyProcess.DataBindings.Add("Text", Model, "StopProcessButtonText", false, DataSourceUpdateMode.OnPropertyChanged);
            btnStopCopyProcess.DataBindings.Add("Enabled", Model, "StopProcessButtonEnabled", false, DataSourceUpdateMode.OnPropertyChanged);
            btnStopCopyProcess.DataBindings.Add("Visible", Model, "StopProcessButtonVisible", false, DataSourceUpdateMode.OnPropertyChanged);
            btnStopCopyProcess.Click += delegate { Invoke(OnCopyStopped); };

            btnGoToPreviousStep.Location = new Point(163, 159);
            btnGoToPreviousStep.DataBindings.Add("Text", Model, "GoToPreviousStepButtonText", false, DataSourceUpdateMode.OnPropertyChanged);
            btnGoToPreviousStep.DataBindings.Add("Visible", Model, "GoToPreviousStepButtonVisible", false, DataSourceUpdateMode.OnPropertyChanged);
            btnGoToPreviousStep.Click += delegate { Invoke(OnPreviousStepClick); };

            btnShowCopiedFiles.Location = new Point(271, 159);
            btnShowCopiedFiles.DataBindings.Add("Text", Model, "ShowCopiedFilesButtonText", false, DataSourceUpdateMode.OnPropertyChanged);
            btnShowCopiedFiles.DataBindings.Add("Visible", Model, "ShowCopiedFilesButtonVisible", false, DataSourceUpdateMode.OnPropertyChanged);
            btnShowCopiedFiles.Click += delegate { Invoke(OnShowCopiedFiles); };
        }

        public void StartCopy()
        {
            Model.StopProcessButtonVisible = true;
            Model.StopProcessButtonEnabled = true;
            Model.ShowCopiedFilesButtonVisible = false;
            Model.GoToPreviousStepButtonVisible = false;
            Model.FormTitle = "Copying Files ... - FileRandomizer3000";
        }

        public void StopCopy()
        {
            Model.StopProcessButtonText = "Terminating...";
            Model.StopProcessButtonEnabled = false;

            Model.FormTitle = "Terminating... - FileRandomizer3000";
        }

        public void CopyStarted()
        {
            Model.ProgressPercentage = 0;

            Model.ProgressInfoText = "";

            Model.StopProcessButtonText = "Terminate";
        }

        public void CopyAborted()
        {
            Model.ProgressInfoText = "Terminated";

            btnGoToPreviousStep.Location = new Point(300, 159);
            Model.GoToPreviousStepButtonText = "Previous step";
            Model.GoToPreviousStepButtonVisible = true;

            Model.ShowCopiedFilesButtonVisible = false;

            Model.StopProcessButtonVisible = false;

            Model.FormTitle = "Copy terminated - FileRandomizer3000";
        }

        public void CopyFinished()
        {
            Model.ProgressInfoText = "Finished";

            Model.GoToPreviousStepButtonText = "Previous step";
            Model.GoToPreviousStepButtonVisible = true;

            Model.ShowCopiedFilesButtonText = "Show copied files";
            Model.ShowCopiedFilesButtonVisible = true;

            Model.StopProcessButtonVisible = false;

            Model.FormTitle = "Copy finished - FileRandomizer3000";
        }

        public void ReportProgress(int progressPercentage)
        {
            Model.ProgressPercentage = progressPercentage;
            Model.FormTitle = string.Format("{0}%", progressPercentage);
        }

        private CopyProcessViewModel Model { get; set; }

        public void AlertErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        void IView.Show()
        {
            if (OnBeforeViewShown != null)
            {
                OnBeforeViewShown();
            }

            Visible = true;
        }

        void IView.Close()
        {
            Visible = false;

            Dispose();
        }

        private void Invoke(Action action)
        {
            if (action != null) action();
        }

        public void SetActiveView()
        {
            _viewHost.ActivateView(this);
        }
    }
}
