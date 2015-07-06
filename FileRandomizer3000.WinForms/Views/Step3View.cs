using FileRandomizer3000.Core.Views;
using FileRandomizer3000.Core.ViewModels;
using System;
using System.Windows.Forms;

namespace FileRandomizer3000.WinForms.Views
{
    public partial class Step3View : UserControl, IStep3View
    {
        private readonly IMainFormViewHost _viewHost;

        public Step3View(IMainFormViewHost viewHost)
        {
            _viewHost = viewHost;
        }

        public event Action OnBeforeViewShown;

        public event Action OnPreviousStepClick;

        public event Action OnNextStepClick;

        private Step3ViewModel Model { get; set; }

        public void Initialize(Step3ViewModel model)
        {
            if (model == null) throw new ArgumentNullException("model");

            Model = model;

            InitializeComponent();

            cmbOnDuplicateActions.DisplayMember = "Title";
            cmbOnDuplicateActions.DataSource = Model.OnDuplicateOptions;
            cmbOnDuplicateActions.DataBindings.Add("SelectedItem", Model, "SelectedOnDuplicateOption", false, DataSourceUpdateMode.OnPropertyChanged);
            picOnDuplicateOptionDescription.DataBindings.Add("Visible", Model, "ShowSelectedOnDuplicateOptionDescription", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbSortOptions.DisplayMember = "Title";
            cmbSortOptions.DataSource = Model.SortOptions;
            cmbSortOptions.DataBindings.Add("SelectedItem", Model, "SelectedSortOption", false, DataSourceUpdateMode.OnPropertyChanged);
            cbxShowRandomizedFiles.DataBindings.Add("Visible", Model, "ShowRandomizedFilesVisible", false, DataSourceUpdateMode.OnPropertyChanged);
            cbxShowRandomizedFiles.DataBindings.Add("Checked", Model, "ShowRandomizedFiles", false, DataSourceUpdateMode.OnPropertyChanged);
            btnProcess.DataBindings.Add("Text", Model, "NextButtonText", false, DataSourceUpdateMode.OnPropertyChanged);

            btnPreviousStep3.Click += delegate { Invoke(OnPreviousStepClick); };

            btnProcess.Click += delegate { Invoke(OnNextStepClick); };

            cbxSaveSettings.DataBindings.Add("Checked", Model, "SaveSettings", false, DataSourceUpdateMode.OnPropertyChanged);

            IsInitialized = true;
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

        private void Invoke(Action action)
        {
            if (action != null) action();
        }
    }
}
