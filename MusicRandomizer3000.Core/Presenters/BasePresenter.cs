using MusicRandomizer3000.Core.Infrastructure.Interfaces;
using MusicRandomizer3000.Core.Views;
using System;

namespace MusicRandomizer3000.Core.Presenters
{
    public abstract class BasePresenter<TView> : IPresenter where TView : IView
    {
        protected TView View { get; private set; }
        protected IApplicationController Controller { get; private set; }

        protected BasePresenter(IApplicationController controller, TView view)
        {
            if (controller == null) throw new ArgumentNullException("controller");
            if (view == null) throw new ArgumentNullException("view");

            Controller = controller;
            View = view;
        }

        public void Run()
        {
            View.Show();
        }
    }

    public abstract class BasePresenter<TView, TArgument> : IPresenter<TArgument> where TView : IView
    {
        protected TView View { get; private set; }
        protected IApplicationController Controller { get; private set; }

        protected BasePresenter(IApplicationController controller, TView view)
        {
            if (controller == null) throw new ArgumentNullException("controller");
            if (view == null) throw new ArgumentNullException("view");

            Controller = controller;
            View = view;
        }

        public abstract void Run(TArgument argument);
    }
}