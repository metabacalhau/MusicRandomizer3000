using MusicRandomizer3000.Core.Infrastructure.Interfaces;
using MusicRandomizer3000.Core.Services;
using MusicRandomizer3000.Core.Services.Interfaces;
using MusicRandomizer3000.Core.ViewModels;
using MusicRandomizer3000.Core.Views;
using System;
using System.Linq;
using System.Threading;

namespace MusicRandomizer3000.Core.Presenters
{
    public class MainFormPresenter : BaseWizardPresenter<IMainFormView>
    {
        private readonly GlobalWizardViewModel _model;

        public MainFormPresenter(IApplicationController controller, IMainFormView view, GlobalWizardViewModel model) : base(controller, view)
        {
            if (model == null) throw new ArgumentNullException("model");

            _model = model;
        }

        public override void Run()
        {
            View.OnActiveViewChanged += ChangeActiveView;

            Controller.RegisterInstance(SynchronizationContext.Current);

            Controller.RunSingleton<Step1Presenter>();

            View.Initialize(_model);
        }

        private void ChangeActiveView(IView oldActiveView, IView newActiveView)
        {
            if (newActiveView == null) throw new ArgumentNullException("newActiveView");

            if (View.Views == null || !View.Views.Any(x => x.GetType() == newActiveView.GetType()))
            {
                View.AddView(newActiveView);
            }

            newActiveView.Show();

            if (oldActiveView != null)
            {
                oldActiveView.Close();
            }
        }
    }
}