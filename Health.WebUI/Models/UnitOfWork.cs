using Health.Domain.Concrete;
using Health.Domain.Entities;
using Health.WebUI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models
{
    public class UnitOfWork : IDisposable
    {
        private static EFDbContext db = new EFDbContext();
        EFGenericRepository<Doctor> repositoryDoctor;
        EFGenericRepository<Specialization> repositorySpecialization;
        EFGenericRepository<Diagnosis> repositoryDiagnosis;
        EFGenericRepository<Disease> repositoryDisease;
        EFGenericRepository<BloodType> repositoryBloodType;
        EFGenericRepository<Gender> repositoryGender;
        EFGenericRepository<Patient> repositoryPatient;
        EFGenericRepository<Appointment> repositoryAppointment;

        public EFGenericRepository<Doctor> Doctors
        {
            get
            {
                if (repositoryDoctor == null)
                    repositoryDoctor = new EFGenericRepository<Doctor>(db);
                return repositoryDoctor;
            }
        }
        public EFGenericRepository<Patient> Patients
        {
            get
            {
                if (repositoryPatient == null)
                    repositoryPatient = new EFGenericRepository<Patient>(db);
                return repositoryPatient;
            }
        }
        public EFGenericRepository<Appointment> Appointments
        {
            get
            {
                if (repositoryAppointment == null)
                    repositoryAppointment = new EFGenericRepository<Appointment>(db);
                return repositoryAppointment;
            }
        }
        public EFGenericRepository<Specialization> Specializations
        {
            get
            {
                if (repositorySpecialization == null)
                    repositorySpecialization = new EFGenericRepository<Specialization>(db);
                return repositorySpecialization;
            }
        }
        public EFGenericRepository<Disease> Diseases
        {
            get
            {
                if (repositoryDisease == null)
                    repositoryDisease = new EFGenericRepository<Disease>(db);
                return repositoryDisease;
            }
        }
        public EFGenericRepository<BloodType> BloodTypes
        {
            get
            {
                if (repositoryBloodType == null)
                    repositoryBloodType = new EFGenericRepository<BloodType>(db);
                return repositoryBloodType;
            }
        }
        public EFGenericRepository<Diagnosis> Diagnoses
        {
            get
            {
                if (repositoryDiagnosis == null)
                    repositoryDiagnosis = new EFGenericRepository<Diagnosis>(db);
                return repositoryDiagnosis;
            }
        }
        public EFGenericRepository<Gender> Genders
        {
            get
            {
                if (repositoryGender == null)
                    repositoryGender = new EFGenericRepository<Gender>(db);
                return repositoryGender;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}