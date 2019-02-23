using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
namespace Mango.Network.Core.Common
{
    public class ServiceContext
    {
        private static System.IServiceProvider _IServiceProvider = null;
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="IServiceProvider"></param>
        public static void RegisterServices(IServiceCollection _IServiceCollection)
        {
            _IServiceProvider = _IServiceCollection.BuildServiceProvider();
        }
        /// <summary>
        /// 获取指定注册服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            if (_IServiceProvider != null)
            {
                return _IServiceProvider.GetService<T>();
            }
            else
            {
                return default(T);
            }
        }
    }
}
