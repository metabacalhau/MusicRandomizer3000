using FileRandomizer3000.Core.Infrastructure.Interfaces;
using FileRandomizer3000.Core.Services.Interfaces;
using FileRandomizer3000.Core.ViewModels;
using FileRandomizer3000.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileRandomizer3000.Core.Presenters
{
    public class Step1Presenter : BaseWizardPresenter<IStep1View>
    {
        private readonly string _formTitle = "Step 1/3";
        private readonly IFolderService _folderService;
        private Step1ViewModel _model;

        public Step1Presenter(IApplicationController controller, IStep1View view, Step1ViewModel model, IFolderService folderService)
            : base(controller, view)
        {
            if (folderService == null) throw new ArgumentNullException("folderService");
            if (model == null) throw new ArgumentNullException("model");

            _folderService = folderService;
            _model = model;
        }

        public override void Run()
        {
            View.OnBeforeViewShown -= OnBeforeViewShownHandler; // to call event handler only once
            View.OnBeforeViewShown += OnBeforeViewShownHandler;

            View.OnFolderBrowserClick -= OpenFolderBrowser; // to call event handler only once
            View.OnFolderBrowserClick += OpenFolderBrowser;

            View.SetActiveView();
        }

        private void Initialize()
        {
            _model.FormTitle = _formTitle;

            View.OnNextStepClick += () =>
            {
                if (_model.PathsFrom == null || _model.PathsFrom.Count == 0)
                {
                    View.ShowPathFromIsEmptyError("Pick a folder");
                }
                else if (!_model.PathsFrom.All(x => _folderService.IsAccessible(x)))
                {
                    View.ShowPathFromIsInaccessible("Selected folders are inaccessible");
                }
                else
                {
                    View.HidePathFromError();
                    View.HideOrganizeFilesDescription();

                    _model.UpdateSettings();
                    _model.UpdateGlobalModel();

                    Controller.RunSingleton<Step2Presenter>();
                }
            };

            View.OnOrganizeFilesDescriptionClick += () =>
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("Folder values: minimum = {0}; maximum = {1}.", _model.FilesAndFolders.FoldersNumberMinimum, _model.FilesAndFolders.FoldersNumberMaximum);
                sb.Append(Environment.NewLine);
                sb.Append("Folders will have \"CD#\" names.");
                sb.Append(Environment.NewLine);
                sb.AppendFormat("File values: minimum = {0}; maximum = {1}.", _model.FilesAndFolders.FilesNumberMinimum, _model.FilesAndFolders.FilesNumberMaximum);

                View.ShowOrganizeFilesDescription(sb.ToString());
            };

            View.Initialize(_model);
        }

        private void Update()
        {
            _model.FormTitle = _formTitle;
        }

        private void OnBeforeViewShownHandler()
        {
            if (View.IsInitialized)
            {
                Update();
            }
            else
            {
                Initialize();
            }
        }

        private void OpenFolderBrowser()
        {
            Controller.Run<FolderBrowserPresenter, List<string>>(_model.PathsFrom);
        }
    }
}