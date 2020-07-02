using Health.Domain.Abstract;
using Health.Domain.Entities;
using Health.WebUI.Models.PatientAppointmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models.MedicalRecord
{
    public class AppointmentData
    {
        IUnitOfWork unitOfWork;
        public Appointment Appointment { get; set; }
        public Diagnosis Diagnosis { get; set; }
        public Disease Disease { get; set; }
        public Specialization Specialization { get; set; }
        public Doctor Doctor { get; set; }

        public AppointmentData(int appointmentId, IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            Appointment = unitOfWork.Appointments.FindById(appointmentId);
            if (Appointment != null)
            {
                Doctor = unitOfWork.Doctors.FindById((int)Appointment.DoctorId);

                if (Doctor != null)
                {
                    Specialization = unitOfWork.Specializations.FindById((int)Doctor.SpecializationId);
                }


                Diagnosis = unitOfWork.Diagnoses.FindById(appointmentId);
                if (Diagnosis == null)
                {
                    Diagnosis = new Diagnosis() 
                    { 
                        DiagnosisId = appointmentId, 
                        DiagnosisComment = "Нет данных",
                        DiagnosisRecommendations = "Нет данных" 
                    };

                    unitOfWork.Diagnoses.Create(Diagnosis);
                    unitOfWork.Save();
                }
                if (Diagnosis.Disease == null)
                {
                    Disease = new Disease() { DiseaseTitle = "Нет данных" };

                }
            }





        }

    }
}