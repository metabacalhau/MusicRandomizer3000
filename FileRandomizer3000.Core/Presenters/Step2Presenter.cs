using FileRandomizer3000.Core.Infrastructure.Interfaces;
using FileRandomizer3000.Core.Services.Interfaces;
using FileRandomizer3000.Core.ViewModels;
using FileRandomizer3000.Core.Views;
using System;
using System.Text;

namespace FileRandomizer3000.Core.Presenters
{
    public class Step2Presenter : BaseWizardPresenter<IStep2View>
    {
        private readonly string _formTitle = "Step 2/3";
        private readonly IFolderService _folderService;
        private Step2ViewModel _model;

        public Step2Presenter(IApplicationController controller, IStep2View view, Step2ViewModel model, IFolderService folderService)
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

            View.SetActiveView();
        }

        private void Initialize()
        {
            _model.FormTitle = _formTitle;

            View.OnNextStepClick += () =>
            {
                if (string.IsNullOrEmpty(_model.PathTo))
                {
                    View.ShowPathToIsEmptyError("Pick a folder");
                }
                else if (!_folderService.IsAccessible(_model.PathTo))
                {
                    View.ShowPathToIsInaccessible("Selected folder is inaccessible");
                }
                else
                {
                    HideViewErrors();

                    _model.UpdateSettings();
                    _model.UpdateGlobalModel();

                    Controller.RunSingleton<Step3Presenter>();
                }
            };

            View.OnCopyActionsDescriptionClick += () =>
            {
                if (_model.SelectedOnCopyOption != null && !string.IsNullOrEmpty(_model.SelectedOnCopyOption.Description))
                {
                    View.ShowOnCopyAddDescription(_model.SelectedOnCopyOption.Description);
                }
            };

            View.OnPreviousStepClick += () =>
            {
                HideViewErrors();

                Controller.RunSingleton<Step1Presenter>();
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

        private void HideViewErrors()
        {
            View.HidePathToError();
            View.HideOnCopyAddDescription();
        }
    }
}