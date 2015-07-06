using FileRandomizer3000.Core.ViewModels;
using FileRandomizer3000.Core.Views;
using System;
using System.Windows.Forms;

namespace FileRandomizer3000.WinForms.Views
{
    public partial class Step2View : UserControl, IStep2View
    {
        private readonly IMainFormViewHost _viewHost;

        public Step2View(IMainFormViewHost viewHost)
        {
            if (viewHost == null) throw new ArgumentNullException("viewHost");

            _viewHost = viewHost;
        }

        public void Initialize(Step2ViewModel model)
        {
            if (model == null) throw new ArgumentNullException("model");

            Model = model;

            InitializeComponent();

            tbxPathTo.DataBindings.Add("Text", Model, "PathTo", false, DataSourceUpdateMode.OnPropertyChanged);
            cbxSaveSettings.DataBindings.Add("Checked", Model, "SaveSettings", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbActionsOnCopy.DisplayMember = "Title";
            cmbActionsOnCopy.DataSource = Model.OnCopyOptions;
            cmbActionsOnCopy.DataBindings.Add("SelectedItem", Model, "SelectedOnCopyOption", false, DataSourceUpdateMode.OnPropertyChanged);
            picOnCopyActionsDescription.DataBindings.Add("Visible", Model, "ShowSelectedOnCopyOptionDescription", false, DataSourceUpdateMode.OnPropertyChanged);

            btnSelectPathTo.Click += delegate
            {
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Model.PathTo = folderBrowserDialog.SelectedPath;
                }
            };

            btnNextStep.Click += delegate { Invoke(OnNextStepClick); };
            btnPreviousStep.Click += delegate { Invoke(OnPreviousStepClick); };
            picOnCopyActionsDescription.Click += delegate { Invoke(OnCopyActionsDescriptionClick); };
            cmbActionsOnCopy.SelectedIndexChanged += delegate { HideOnCopyAddDescription(); };

            IsInitialized = true;
        }

        public event Action OnPreviousStepClick;

        public event Action OnNextStepClick;

        public event Action OnBeforeViewShown;

        public event Action OnCopyActionsDescriptionClick;

        public void ShowPathToIsEmptyError(string errorMessage)
        {
            ShowPathToError(errorMessage);
        }

        public void ShowPathToIsInaccessible(string errorMessage)
        {
            ShowPathToError(errorMessage);
        }

        public void HidePathToError()
        {
            if (tooltip != null)
            {
                tooltip.Hide(btnSelectPathTo);
                tooltip.Dispose();
                tooltip = null;
            }
        }

        public void ShowOnCopyAddDescription(string description)
        {
            HideOnCopyAddDescription();

            tooltip = new ToolTip();
            tooltip.IsBalloon = true;
            tooltip.ShowAlways = true;
            tooltip.Active = true;
            tooltip.Show(description, picOnCopyActionsDescription, 0, -40);
        }

        public void HideOnCopyAddDescription()
        {
            if (tooltip != null)
            {
                tooltip.Hide(picOnCopyActionsDescription);
                tooltip.Dispose();
                tooltip = null;
            }
        }

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
        }

        public void SetActiveView()
        {
            _viewHost.ActivateView(this);
        }

        public bool IsInitialized { get; private set; }

        private void ShowPathToError(string errorMessage)
        {
            HidePathToError();

            tooltip = new ToolTip();
            tooltip.IsBalloon = true;
            tooltip.ShowAlways = true;
            tooltip.Active = true;
            tooltip.Show(errorMessage, btnSelectPathTo, 10, -40);
        }

        private Step2ViewModel Model { get; set; }

        private void ShowHideOrganizeFilesPanel(bool show)
        {
            //pnlOrganizeFiles.Visible = show;
        }

        private void Invoke(Action action)
        {
            if (action != null) action();
        }
    }
}