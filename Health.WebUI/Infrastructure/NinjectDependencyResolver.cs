using System;
using System.Collections.Generic;
using Ninject;
using Health.Domain.Entities;
using Health.Domain.Abstract;
using Health.Domain.Concrete;

namespace Health.WebUI.Infrastructure
{
    public class NinjectDependencyResolver
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
            kernel.Bind<IRepository<Patient>>().To<EFPatientRepository>();
          
        }
    }
}