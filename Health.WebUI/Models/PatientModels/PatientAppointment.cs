using Health.Domain.Abstract;
using Health.Domain.Concrete;
using Health.Domain.Entities;
using Health.WebUI.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models.PatientModels
{
    public class PatientAppointment
    {

        //EFGenericRepository<Doctor> repositoryDoctor = new EFGenericRepository<Doctor>(new EFDbContext());
        //EFGenericRepository<Patient> repositoryPatient = new EFGenericRepository<Patient>(new EFDbContext());
        //EFGenericRepository<Specialization> repositorySpecialization = new EFGenericRepository<Specialization>(new EFDbContext());
        //EFGenericRepository<Diagnosis> repositoryDiagnosis = new EFGenericRepository<Diagnosis>(new EFDbContext());
        //EFGenericRepository<Disease> repositoryDisease = new EFGenericRepository<Disease>(new EFDbContext());
        //EFGenericRepository<BloodType> repositoryBloodType = new EFGenericRepository<BloodType>(new EFDbContext());
        //EFGenericRepository<Gender> repositoryGender = new EFGenericRepository<Gender>(new EFDbContext());
        UnitOfWork unitOfWork;

        public Doctor Doctor { get; set; }
        public Specialization Specialization { get; set; }
        public Disease Disease { get; set; }
        public Appointment Appointment { get; set; }
        public Diagnosis Diagnosis { get; set; }


        public PatientAppointment( Appointment appointment)
        {
            unitOfWork = new UnitOfWork();
            Appointment = unitOfWork.Appointments.FindById(appointment.AppointmentId);
            if(appointment.DoctorId!=null)
            Doctor = unitOfWork.Doctors.FindById((int)appointment.DoctorId);
            Diagnosis = unitOfWork.Diagnoses.FindById((int)appointment.DoctorId);
            Specialization = unitOfWork.Specializations.FindById((int)Doctor.DoctorId);
            if (Diagnosis!= null&& Diagnosis.Disease!=null)
            Disease = unitOfWork.Diseases.FindById((int)Diagnosis.DiseaseId); 
      

       
            }
   



        }
    }

