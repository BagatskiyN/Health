using Health.Domain.Concrete;
using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Abstract
{
   public interface IUnitOfWork
    {
        IGenericRepository<Doctor> Doctors { get; }
        IGenericRepository<Appointment> Appointments { get; }
        IGenericRepository<Specialization> Specializations { get; }
        IGenericRepository<Gender> Genders { get; }
        IGenericRepository<Patient> Patients { get; }

        IGenericRepository<Disease> Diseases { get; }
        IGenericRepository<BloodType> BloodTypes { get; }
        void Save();
       void Dispose(bool disposing);
        void Dispose();

     }
}
