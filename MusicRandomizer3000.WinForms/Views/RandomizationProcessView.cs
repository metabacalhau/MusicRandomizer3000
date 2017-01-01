using MusicRandomizer3000.Core.ViewModels;
using MusicRandomizer3000.Core.Views;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MusicRandomizer3000.WinForms.Views
{
    public partial class RandomizationProcessView : UserControl, IRandomizationProcessView
    {
        private readonly IMainFormViewHost _viewHost;

        public RandomizationProcessView(IMainFormViewHost viewHost)
        {
            _viewHost = viewHost;
        }

        public event Action OnBeforeViewShown;

        public event Action OnRandomizationStopped;

        public event Action OnRandomizationAborted;

        private RandomizationProcessViewModel Model { get; set; }

        public void Initialize(RandomizationProcessViewModel model)
        {
            if (model == null) throw new ArgumentNullException("model");

            Model = model;

            InitializeComponent();

            imgLoader.DataBindings.Add("Visible", Model, "SpinnerImageVisible", false, DataSourceUpdateMode.OnPropertyChanged);

            lblInfo.DataBindings.Add("Text", Model, "InfoText", false, DataSourceUpdateMode.OnPropertyChanged);
            lblInfo.DataBindings.Add("Visible", Model, "InfoVisible", false, DataSourceUpdateMode.OnPropertyChanged);

            btnStopRandomizationProcess.Location = new Point(300, 159);
            btnStopRandomizationProcess.DataBindings.Add("Text", Model, "StopProcessButtonText", false, DataSourceUpdateMode.OnPropertyChanged);
            btnStopRandomizationProcess.DataBindings.Add("Enabled", Model, "StopProcessButtonEnabled", false, DataSourceUpdateMode.OnPropertyChanged);
            btnStopRandomizationProcess.DataBindings.Add("Visible", Model, "StopProcessButtonVisible", false, DataSourceUpdateMode.OnPropertyChanged);
            btnStopRandomizationProcess.Click += delegate { Invoke(OnRandomizationStopped); };

            btnGoToPreviousStep.Location = new Point(300, 159);
            btnGoToPreviousStep.DataBindings.Add("Text", Model, "GoToPreviousStepButtonText", false, DataSourceUpdateMode.OnPropertyChanged);
            btnGoToPreviousStep.DataBindings.Add("Visible", Model, "GoToPreviousStepButtonVisible", false, DataSourceUpdateMode.OnPropertyChanged);
            btnGoToPreviousStep.Click += delegate { Invoke(OnRandomizationAborted); };
        }

        public void StartRandomization()
        {
            Model.FormTitle = "Randomizing files ...";
            Model.StopProcessButtonText = "Terminate";
            Model.InfoText = "";
            Model.InfoVisible = false;
            Model.SpinnerImageVisible = true;
            Model.StopProcessButtonVisible = true;
            Model.GoToPreviousStepButtonVisible = false;
        }

        public void StopRandomization()
        {
            Model.FormTitle = "Terminating...";
            Model.StopProcessButtonText = "Terminating...";
            Model.StopProcessButtonEnabled = false;
        }

        public void RandomizationAborted()
        {
            Invoke(OnRandomizationAborted);

            Model.StopProcessButtonEnabled = true;
        }

        public void AlertErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        public void NothingToCopy()
        {
            Model.FormTitle = "Nothing to copy";
            Model.InfoText = "Nothing to copy";
            Model.InfoVisible = true;
            Model.SpinnerImageVisible = false;
            Model.StopProcessButtonVisible = false;
            Model.GoToPreviousStepButtonText = "Previous step";
            Model.GoToPreviousStepButtonVisible = true;
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

        public void SetActiveView()
        {
            _viewHost.ActivateView(this);
        }

        private void Invoke(Action action)
        {
            if (action != null) action();
        }
    }
}