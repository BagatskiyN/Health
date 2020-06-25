using System;
using System.Collections.Generic;
using Ninject;
using Health.Domain.Entities;
using Health.Domain.Abstract;
using Health.Domain.Concrete;
using System.Web.Mvc;
using System.Data.Entity;
using Health.Domain.Entities;
using Ninject.Web.Common;
using Health.WebUI.Models;

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
            kernel.Bind<IRepository<Patient>>().To<EFPatientRepository>();
            kernel.Bind<IRepository<Appointment>>().To<EFAppointmentRepository>();
            kernel.Bind<IRepository<Doctor>>().To<EFDoctorRepository>();
            kernel.Bind<IRepository<Disease>>().To<EFDiseaseRepository>();
            kernel.Bind<IRepository<BloodType>>().To<EFBloodTypeRepository>();
            kernel.Bind<IRepository<Gender>>().To<EFGenderRepository>();
            kernel.Bind<IRepository<Specialization>>().To<EFSpecializationRepository>();
            kernel.Bind<IGenericRepository<Patient>>().To<EFGenericRepository<Patient>>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}