using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Moq;
using Ninject;

using Garden.Domain.Entities;
using Garden.Domain.Abstract;
using Garden.Domain.Concrete;
using Garden.WebUI.Infrastructure.Abstract;
using Garden.WebUI.Infrastructure.Concrete;

namespace Garden.WebUI.Infrastructure
{
    //требуется для правильной работы библиотеки Ninject
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            // Здесь размещаются привязки
            kernel.Bind<IMessageRepository>().To<EFMessageRepository>();
            kernel.Bind<ITopicRepository>().To<EFTopicRepository>();
            kernel.Bind<ICategoryRepository>().To<EFCategoryRepository>();
            kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
        }
    }
}