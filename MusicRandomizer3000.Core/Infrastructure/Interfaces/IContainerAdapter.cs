namespace MusicRandomizer3000.Core.Infrastructure.Interfaces
{
    public interface IContainerAdapter
    {
        void Register<TService, TImplementation>() where TImplementation : TService;
        void RegisterSingleton<TService, TImplementation>() where TImplementation : TService;
        void Register<TService>();
        void RegisterSingleton<TService>();
        void RegisterInstance<T>(T instance) where T : class;
        void RegisterSingletonInstance<T>(T instance) where T : class;
        TService Resolve<TService>();
        bool IsRegistered<TService>();
    }
}