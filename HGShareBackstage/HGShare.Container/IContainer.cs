using System;
using Autofac.Core;

namespace HGShare.Container
{
    /// <summary>
    /// IOC容器接口
    /// </summary>
    public interface IContainer : IServiceProvider, IServiceRegister
    {

    }

    /// <summary>
    /// 提供服务实现
    /// </summary>
    public interface IServiceProvider
    {
        /// <summary>
        /// 获取一个服务的实例，所有的服务都是单例
        /// </summary>
        /// <typeparam name="TService">返回的服务的类型</typeparam>
        /// <returns>服务的实现</returns>
        TService Resolve<TService>() where TService : class;
        /// <summary>
        /// 获取一个服务的实例，所有的服务都是单例
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="parameterValue">参数值</param>
        /// <typeparam name="TService">返回的服务的类型</typeparam>
        /// <returns>服务的实现</returns>
        TService Resolve<TService>(string parameterName, object parameterValue) where TService : class;
        /// <summary>
        /// 获取一个服务的实例，所有的服务都是单例
        /// </summary>
        /// <param name="parameters">参数集合</param>
        /// <typeparam name="TService">返回的服务的类型</typeparam>
        /// <returns>服务的实现</returns>
        TService Resolve<TService>(Parameter[] parameters) where TService : class;
    }

    /// <summary>
    /// 注册服务
    /// </summary>
    public interface IServiceRegister
    {
        /// <summary>
        /// 注册服务的方法，同样服务只有第一个注册有效，后来的注册会被忽略
        /// </summary>
        /// <typeparam name="TService">注册服务的类型</typeparam>
        /// <param name="serviceCreator">一个函数：可以创建出一个服务类型的实现</param>
        /// <returns>返回自身，以支持链式调用</returns>
        IServiceRegister Register<TService>(Func<IServiceProvider, TService> serviceCreator) where TService : class;

        /// <summary>
        /// 注册服务的方法，同样服务只有第一个注册有效，后来的注册会被忽略
        /// </summary>
        /// <typeparam name="TService">注册的服务的类型</typeparam>
        /// <typeparam name="TImplementation">对应的服务的实现</typeparam>
        /// <returns>返回自身，以支持链式调用</returns>
        IServiceRegister Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;
    }
}
