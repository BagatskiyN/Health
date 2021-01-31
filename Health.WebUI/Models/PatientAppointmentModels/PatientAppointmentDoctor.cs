using Health.Domain.Abstract;
using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models.PatientAppointmentModels
{
    public class PatientAppointmentDoctor
    {
        public Doctor Doctor { get; set; }
        public Specialization Specialization { get; set; }
      
        public PatientAppointmentDoctor(int doctorId, IUnitOfWork _unitOfWork)
        {
          
            Doctor = _unitOfWork.Doctors.FindById(doctorId);
            if (Doctor != null)
            {
                Specialization = _unitOfWork.Specializations.FindById((int)Doctor.SpecializationId);
            }
            else
            {
                throw new Exception("Доктор не найден!");
            }


        }


    }
}