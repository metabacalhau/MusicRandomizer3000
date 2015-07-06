using FileRandomizer3000.Core.Infrastructure.Interfaces;
using FileRandomizer3000.Core.ViewModels;
using FileRandomizer3000.Core.Views;
using System;

namespace FileRandomizer3000.Core.Presenters
{
    public abstract class BaseWizardPresenter<TView> : IPresenter where TView : IView
    {
        protected TView View { get; private set; }
        protected IApplicationController Controller { get; private set; }

        protected BaseWizardPresenter(IApplicationController controller, TView view)
        {
            if (controller == null) throw new ArgumentNullException("controller");
            if (view == null) throw new ArgumentNullException("view");

            Controller = controller;
            View = view;
        }

        public abstract void Run();
    }

    public abstract class BaseWizardPresenter<TView, TArgument> : IPresenter<TArgument> where TView : IView
    {
        protected TView View { get; private set; }
        protected IApplicationController Controller { get; private set; }

        protected BaseWizardPresenter(IApplicationController controller, TView view)
        {
            if (controller == null) throw new ArgumentNullException("controller");
            if (view == null) throw new ArgumentNullException("view");

            Controller = controller;
            View = view;
        }

        public abstract void Run(TArgument argument);
    }
}