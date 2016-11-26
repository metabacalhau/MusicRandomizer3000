using FileRandomizer3000.Core.Enums;
using FileRandomizer3000.Core.ViewModels;
using FileRandomizer3000.Core.Views;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FileRandomizer3000.WinForms.Views
{
    public partial class Step1View : UserControl, IStep1View
    {
        private readonly IMainFormViewHost _viewHost;

        public Step1View(IMainFormViewHost viewHost)
        {
            if (viewHost == null) throw new ArgumentNullException("viewHost");

            _viewHost = viewHost;
        }

        public void Initialize(Step1ViewModel model)
        {
            if (model == null) throw new ArgumentNullException("model");

            Model = model;

            InitializeComponent();

            cbxFindOnlyUniqueFiles.DataBindings.Add("Checked", Model, "FindOnlyUniqueFiles", false, DataSourceUpdateMode.OnPropertyChanged);
            cbxSaveSettings.DataBindings.Add("Checked", Model, "SaveSettings", false, DataSourceUpdateMode.OnPropertyChanged);
            cmbFilesLimitTypes.DisplayMember = "Title";
            cmbFilesLimitTypes.DataSource = Model.FilesLimitTypes;
            cmbFilesLimitTypes.DataBindings.Add("SelectedItem", Model, "SelectedFilesLimit", false, DataSourceUpdateMode.OnPropertyChanged);

            cmbUnits.DisplayMember = "Title";
            cmbUnits.DataSource = Model.FilesSize.Sizes;
            cmbUnits.DataBindings.Add("SelectedItem", Model.FilesSize, "SelectedSize", false, DataSourceUpdateMode.OnPropertyChanged);
            tbxSizeLimit.DataBindings.Add("Text", Model.FilesSize, "SizeLimit", false, DataSourceUpdateMode.OnPropertyChanged);
            lblSizeRange.DataBindings.Add("Text", Model.FilesSize, "SizeRange", false, DataSourceUpdateMode.OnPropertyChanged);

            numFilesNumber.DataBindings.Add("Minimum", Model.FilesNumber, "Minimum", false, DataSourceUpdateMode.OnPropertyChanged);
            numFilesNumber.DataBindings.Add("Maximum", Model.FilesNumber, "Maximum", false, DataSourceUpdateMode.OnPropertyChanged);
            numFilesNumber.DataBindings.Add("Value", Model.FilesNumber, "Number", false, DataSourceUpdateMode.OnPropertyChanged);
            lblLimitRange.DataBindings.Add("Text", Model.FilesNumber, "Range", false, DataSourceUpdateMode.OnPropertyChanged);

            numFolders.DataBindings.Add("Value", Model.FilesAndFolders, "FoldersNumber", false, DataSourceUpdateMode.OnPropertyChanged);
            numFolders.DataBindings.Add("Minimum", Model.FilesAndFolders, "FoldersNumberMinimum", false, DataSourceUpdateMode.OnPropertyChanged);
            numFolders.DataBindings.Add("Maximum", Model.FilesAndFolders, "FoldersNumberMaximum", false, DataSourceUpdateMode.OnPropertyChanged);
            numFiles.DataBindings.Add("Value", Model.FilesAndFolders, "FilesNumber", false, DataSourceUpdateMode.OnPropertyChanged);
            numFiles.DataBindings.Add("Minimum", Model.FilesAndFolders, "FilesNumberMinimum", false, DataSourceUpdateMode.OnPropertyChanged);
            numFiles.DataBindings.Add("Maximum", Model.FilesAndFolders, "FilesNumberMaximum", false, DataSourceUpdateMode.OnPropertyChanged);

            btnSelectPathFrom.Click += delegate { Invoke(OnFolderBrowserClick); };
            cmbFilesLimitTypes.SelectedIndexChanged += delegate { Invoke(ShowFilesLimitBlock); };
            btnNextStep1.Click += delegate { Invoke(OnNextStepClick); };
            picOrganizeFilesDescription.Click += delegate { Invoke(OnOrganizeFilesDescriptionClick); };

            ShowFilesLimitBlock();

            IsInitialized = true;
        }

        private Step1ViewModel Model { get; set; }

        public event Action OnFolderBrowserClick;

        public event Action OnNextStepClick;

        public event Action OnBeforeViewShown;

        public event Action OnOrganizeFilesDescriptionClick;

        public void ShowPathFromIsEmptyError(string errorMessage)
        {
            ShowPathFromError(errorMessage);
        }

        public void ShowPathFromIsInaccessible(string errorMessage)
        {
            ShowPathFromError(errorMessage);
        }

        public void AlertErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        private void ShowPathFromError(string errorMessage)
        {
            HidePathFromError();

            tooltip = new ToolTip();
            tooltip.IsBalloon = true;
            tooltip.ShowAlways = true;
            tooltip.Active = true;
            tooltip.Show(errorMessage, btnSelectPathFrom, 10, -40);
        }

        public void HidePathFromError()
        {
            if (tooltip != null)
            {
                tooltip.Hide(btnSelectPathFrom);
                tooltip.Dispose();
                tooltip = null;
            }
        }

        public void ShowOrganizeFilesDescription(string description)
        {
            HideOrganizeFilesDescription();

            tooltip = new ToolTip();
            tooltip.IsBalloon = true;
            tooltip.ShowAlways = true;
            tooltip.Active = true;
            tooltip.Show(description, picOrganizeFilesDescription, 0, -80);
        }

        public void HideOrganizeFilesDescription()
        {
            if (tooltip != null)
            {
                tooltip.Hide(picOrganizeFilesDescription);
                tooltip.Dispose();
                tooltip = null;
            }
        }

        void IView.Show()
        {
            Invoke(OnBeforeViewShown);

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

        private void ShowFilesLimitBlock()
        {
            if (Model.SelectedFilesLimit != null)
            {
                if (Model.SelectedFilesLimit.ID == (int)LimitType.FilesTotalSize)
                {
                    pnlByTotalSize.Location = new Point(42, 113);
                    pnlByTotalSize.Visible = true;
                    pnlByNumber.Visible = false;
                    pnlByFolders.Visible = false;
                }
                else if (Model.SelectedFilesLimit.ID == (int)LimitType.FilesNumber)
                {
                    pnlByNumber.Location = new Point(63, 113);
                    pnlByNumber.Visible = true;
                    pnlByTotalSize.Visible = false;
                    pnlByFolders.Visible = false;
                }
                else if (Model.SelectedFilesLimit.ID == (int)LimitType.FilesNumberPerFolder)
                {
                    pnlByFolders.Location = new Point(40, 113);
                    pnlByFolders.Visible = true;
                    pnlByNumber.Visible = false;
                    pnlByTotalSize.Visible = false;
                }
            }
        }

        private void Invoke(Action action)
        {
            if (action != null) action();
        }
    }
}