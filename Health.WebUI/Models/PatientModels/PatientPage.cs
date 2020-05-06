using Health.Domain.Abstract;
using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models.PatientModels
{
    public class PatientPage
    {   private IRepository<Patient> repositoryPatient;
        private IRepository<Doctor> repositoryDoctor;
        private IRepository<Appointment> repositoryAppointment;
        private IRepository<Specialization> repositorySpecialization;
        private IRepository<Gender> repositoryGender;
        private IRepository<BloodType> repositoryBloodType;
        public Patient Patient { get; set; }
        public Specialization Specialization { get; set; }
        public Gender PatientGender { get; set; }
        public BloodType PatientBloodType { get; set; }
        public IEnumerable<PatientAppointment> PreviousAppointments { get; set; }
        public IEnumerable<PatientAppointment> NextAppointments { get; set; }

        
        public PatientPage(IRepository<Patient> _repositoryPatient,IRepository<Doctor> repositoryDoctor,IRepository<Appointment> _repositoryAppointment,
            IRepository<Gender> repository,IRepository<BloodType> repositoryBloodType)
        {

        }

    }
}