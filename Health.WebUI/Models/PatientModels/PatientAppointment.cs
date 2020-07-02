using Health.Domain.Abstract;
using Health.Domain.Concrete;
using Health.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models.PatientModels
{
    public class PatientAppointment
    {

        UnitOfWork unitOfWork;

        public Doctor Doctor { get; set; }
        public Specialization Specialization { get; set; }
     
        public Appointment Appointment { get; set; }
     


        public PatientAppointment( Appointment appointment)
        {
            unitOfWork = new UnitOfWork();
            Appointment = unitOfWork.Appointments.FindById(appointment.AppointmentId);
            if(appointment.DoctorId!=null)
            Doctor = unitOfWork.Doctors.FindById((int)appointment.DoctorId);

            Specialization = unitOfWork.Specializations.FindById((int)Doctor.SpecializationId);
    
      

       
            }
   



        }
    }

