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
       public IUnitOfWork unitOfWork;
        public PatientAppointmentDoctor(int doctorId,IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            Doctor = unitOfWork.Doctors.FindById(doctorId);
            Specialization = unitOfWork.Specializations.FindById((int)Doctor.SpecializationId);

        }
       

    }
}