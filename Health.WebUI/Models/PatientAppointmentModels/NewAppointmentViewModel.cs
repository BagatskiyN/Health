using Health.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Health.WebUI.Models.PatientAppointmentModels
{
    public class NewAppointmentViewModel
    {
        public Doctor Doctor { get; set; }
        
        public Specialization Specialization { get; set; }

        public Appointment Appointment { get; set; }
        public Diagnosis Diagnosis { get; set; }

        public List<DayOfAppointment> daysOfWeek { get; set; }
        UnitOfWork unitOfWork;


        public NewAppointmentViewModel(int doctorId,int patientId)
        { unitOfWork = new UnitOfWork();
            Appointment = new Appointment();
          
            Appointment.AppointmentPlace = (doctorId * 12).ToString();
            Appointment.Patient = unitOfWork.Patients.FindById(patientId);
            Appointment.PatientId = patientId;
            Appointment.Doctor = unitOfWork.Doctors.FindById(doctorId);
            Doctor= unitOfWork.Doctors.FindById(doctorId);
            Appointment.DoctorId = doctorId;
            Specialization = unitOfWork.Specializations.FindById((int)Doctor.SpecializationId);
            daysOfWeek = FullInDayOfAppointments();

        }
        public List<DayOfAppointment> FullInDayOfAppointments ()
        { List<DayOfAppointment> dayOfAppointments = new List<DayOfAppointment>();
            for(int i=1;i<=7;i++)
            {if(DateTime.Now.AddDays(i).DayOfWeek!=DayOfWeek.Saturday&&DateTime.Now.AddDays(i).DayOfWeek!=DayOfWeek.Sunday)
                {
                    dayOfAppointments.Add(new DayOfAppointment(i, Doctor.Id));
                }

            }
            return dayOfAppointments;
        }
    }

}