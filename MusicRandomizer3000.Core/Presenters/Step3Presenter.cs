using MusicRandomizer3000.Core.Infrastructure.Interfaces;
using MusicRandomizer3000.Core.ViewModels;
using MusicRandomizer3000.Core.Views;
using System;

namespace MusicRandomizer3000.Core.Presenters
{
    public class Step3Presenter : BaseWizardPresenter<IStep3View>
    {
        private readonly string _formTitle = "Step 3/3";
        private Step3ViewModel _model;

        public Step3Presenter(IApplicationController controller, IStep3View view, Step3ViewModel model)
            : base(controller, view)
        {
            if (model == null) throw new ArgumentNullException("model");

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

            View.OnPreviousStepClick += () =>
            {
                Controller.RunSingleton<Step2Presenter>();
            };

            View.OnNextStepClick += () =>
            {
                _model.UpdateSettings();
                _model.UpdateGlobalModel();

                Controller.Run<RandomizationProcessPresenter>();
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
    }
}