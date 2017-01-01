using MusicRandomizer3000.Core.Infrastructure.Interfaces;
using StructureMap;

namespace MusicRandomizer3000.Core.Infrastructure
{
    public class StructureMapAdapter : IContainerAdapter
    {
        private readonly IContainer _container = new Container();

        public void Register<TService, TImplementation>() where TImplementation : TService
        {
            _container.Configure(c => c.For<TService>().Use<TImplementation>());
        }

        public void RegisterSingleton<TService, TImplementation>() where TImplementation : TService
        {
            _container.Configure(c => c.For<TService>().Singleton().Use<TImplementation>());
        }

        public void Register<TService>()
        {
            _container.Configure(c => c.ForConcreteType<TService>());
        }

        public void RegisterSingleton<TService>()
        {
            _container.Configure(c => c.ForSingletonOf<TService>().Use<TService>());
        }

        public void RegisterInstance<T>(T instance) where T : class
        {
            _container.Configure(c => c.For<T>().Use(instance));
        }

        public void RegisterSingletonInstance<T>(T instance) where T : class
        {
            _container.Configure(c => c.ForSingletonOf<T>().Use(instance));
        }

        public TService Resolve<TService>()
        {
            return _container.GetInstance<TService>();
        }

        public bool IsRegistered<TService>()
        {
            return _container.Model.HasImplementationsFor<TService>();
        }
    }
}