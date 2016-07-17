using FileRandomizer3000.Core.Infrastructure.Interfaces;
using FileRandomizer3000.Core.Presenters;
using FileRandomizer3000.Core.Views;
using System;

namespace FileRandomizer3000.Core.Infrastructure
{
    public class ApplicationController : IApplicationController
    {
        private readonly IContainerAdapter _container;

        public ApplicationController(IContainerAdapter container)
        {
            if (container == null) throw new ArgumentNullException("container");

            _container = container;
            _container.RegisterInstance<IApplicationController>(this);
        }

        public IApplicationController RegisterView<TView, TImplementation>()
            where TImplementation : class, TView
            where TView : IView
        {
            _container.Register<TView, TImplementation>();
            return this;
        }

        public IApplicationController RegisterSingletonView<TView, TImplementation>()
            where TView : IView
            where TImplementation : class, TView
        {
            _container.RegisterSingleton<TView, TImplementation>();
            return this;
        }

        public IApplicationController RegisterInstance<TImplementation>(TImplementation instance) where TImplementation : class
        {
            _container.RegisterInstance(instance);
            return this;
        }

        public IApplicationController RegisterSingletonInstance<TImplementation>(TImplementation instance) where TImplementation : class
        {
            _container.RegisterSingletonInstance(instance);
            return this;
        }

        public IApplicationController RegisterService<TService, TImplementation>() where TImplementation : class, TService
        {
            _container.Register<TService, TImplementation>();
            return this;
        }

        public IApplicationController RegisterSingletonService<TService, TInstance>() where TInstance : class, TService
        {
            _container.RegisterSingleton<TService, TInstance>();
            return this;
        }

        public void Run<TPresenter>() where TPresenter : class, IPresenter
        {
            if (!_container.IsRegistered<TPresenter>())
            {
                _container.Register<TPresenter>();
            }

            var presenter = _container.Resolve<TPresenter>();
            presenter.Run();
        }

        public void Run<TPresenter, TArgument>(TArgument argument) where TPresenter : class, IPresenter<TArgument>
        {
            if (!_container.IsRegistered<TPresenter>())
            {
                _container.Register<TPresenter>();
            }

            var presenter = _container.Resolve<TPresenter>();
            presenter.Run(argument);
        }

        public void RunSingleton<TPresenter>() where TPresenter : class, IPresenter
        {
            if (!_container.IsRegistered<TPresenter>())
            {
                _container.RegisterSingleton<TPresenter>();
            }

            var presenter = _container.Resolve<TPresenter>();
            presenter.Run();
        }

        public void RunSingleton<TPresenter, TArgument>(TArgument argument) where TPresenter : class, IPresenter<TArgument>
        {
            if (!_container.IsRegistered<TPresenter>())
            {
                _container.RegisterSingleton<TPresenter>();
            }

            var presenter = _container.Resolve<TPresenter>();
            presenter.Run(argument);
        }
    }
}