using System;
using Autofac;
using HGShare.Utils.Interface;
using IContainer = HGShare.Utils.Interface.IContainer;
using IServiceProvider = HGShare.Utils.Interface.IServiceProvider;

namespace HGShare.Container
{
    public class AutofacAdapter : IContainer, IDisposable
    {
        private ContainerBuilder _initialBuilder;
        private Autofac.IContainer _container;
        private bool _ownsContainer;

        public AutofacAdapter(ContainerBuilder initialBuilder = null)
        {
            _initialBuilder = initialBuilder ?? new ContainerBuilder();

            _initialBuilder
                .RegisterInstance(this)
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();
        }

        public Autofac.IContainer Container
        {
            get
            {
                if (_container != null)
                    return _container;

                _container = _initialBuilder.Build();
                _initialBuilder = null;
                _ownsContainer = true;

                return _container;
            }
            set { _container = value; }
        }

        public IServiceRegister Register<TService>(Func<IServiceProvider, TService> serviceCreator) where TService : class
        {
            if (serviceCreator == null)
                throw new ArgumentNullException("serviceCreator");

            var builder = _initialBuilder ?? new ContainerBuilder();

            builder
                .Register(c => serviceCreator(this))
                .SingleInstance();

            if (_container != null && !_container.IsRegistered<TService>())
                builder.Update(_container);

            return this;
        }

        public IServiceRegister Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            var builder = _initialBuilder ?? new ContainerBuilder();

            builder
                .RegisterType<TImplementation>()
                .As<TService>()
                .SingleInstance();

            if (_container != null && !_container.IsRegistered<TService>())
                builder.Update(_container);

            return this;
        }

        public TService Resolve<TService>() where TService : class
        {
            return Container.Resolve<TService>();
        }

        public TService Resolve<TService>(string parameterName,object parameterValue) where TService : class
        {
            return Container.Resolve<TService>(new NamedParameter(parameterName,parameterValue));
        }

        //public TService Resolve<TService>(Parameter[] parameters) where TService : class
        //{
        //    return Container.Resolve<TService>(parameters);
        //}
        public void Dispose()
        {
            if (_ownsContainer && _container != null)
                _container.Dispose();
        }
    }
}
