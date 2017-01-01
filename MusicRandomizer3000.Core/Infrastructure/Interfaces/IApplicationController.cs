using MusicRandomizer3000.Core.Presenters;
using MusicRandomizer3000.Core.Views;

namespace MusicRandomizer3000.Core.Infrastructure.Interfaces
{
    public interface IApplicationController
    {
        IApplicationController RegisterView<TView, TImplementation>()
            where TImplementation : class, TView
            where TView : IView;
        IApplicationController RegisterSingletonView<TView, TImplementation>()
            where TImplementation : class, TView
            where TView : IView;
        IApplicationController RegisterInstance<TInstance>(TInstance instance)
            where TInstance : class;
        IApplicationController RegisterSingletonInstance<TInstance>(TInstance instance)
            where TInstance : class;
        IApplicationController RegisterService<TService, TInstance>()
            where TInstance : class, TService;
        IApplicationController RegisterSingletonService<TService, TInstance>()
            where TInstance : class, TService;
        void Run<TPresenter>()
            where TPresenter : class, IPresenter;
        void Run<TPresenter, TArgument>(TArgument argument)
            where TPresenter : class, IPresenter<TArgument>;
        void RunSingleton<TPresenter>()
            where TPresenter : class, IPresenter;
        void RunSingleton<TPresenter, TArgument>(TArgument argument)
            where TPresenter : class, IPresenter<TArgument>;
    }
}