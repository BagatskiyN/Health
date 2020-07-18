using Health.Domain.Abstract;
using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models.DoctorProfile
{
    public class DoctorProfileAppointment
    {
        IUnitOfWork unitOfWork;
        public Patient Patient { get; set; }

        public Appointment Appointment { get; set; }

        public DoctorProfileAppointment(IUnitOfWork _unitOfWork,int appointmentId)
        {
            unitOfWork = _unitOfWork;
            Appointment = unitOfWork.Appointments.FindById(appointmentId);
            if(Appointment!=null)
            {
                Patient = unitOfWork.Patients.FindById((int)Appointment.PatientId);
            }

        }
    }
}