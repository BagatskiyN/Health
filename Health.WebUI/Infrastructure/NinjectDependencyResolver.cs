using System;
using System.Collections.Generic;
using Ninject;
using Health.Domain.Entities;
using Health.Domain.Abstract;
using Health.Domain.Concrete;
using System.Web.Mvc;
using System.Data.Entity;
using Ninject.Web.Common;
using Health.WebUI.Models;
using Health.WebUI.Controllers;
using Health.WebUI.Abstract;

namespace Health.WebUI.Infrastructure
{
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
           
            kernel.Bind<IPictureManipulator>().To<ImageController>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}