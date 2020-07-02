﻿using Health.Domain.Abstract;
using Health.Domain.Entities;
using Health.WebUI.Models.PatientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models.MedicalRecord
{
    public class SpecializationRecordsListViewModel
    {
        IUnitOfWork unitOfWork;
       public Specialization Specialization;
       public List<PatientAppointment> Appointments=new List<PatientAppointment>();
        int specializationId;
        int pageSize = 6;
        public SpecializationRecordsListViewModel(IUnitOfWork _unitOfWork,int _specializationId)
        {
            specializationId = _specializationId;
            unitOfWork = _unitOfWork;
            Specialization = unitOfWork.Specializations.FindById(specializationId);
        }
        public List<PatientAppointment> GetAppointmentsBySpecialization(int page=0)
        {
            var skipRecords = page * pageSize;
            List<PatientAppointment> appointments = unitOfWork.Appointments
                .Get().Where(m => (unitOfWork.Doctors.FindById((int)m.DoctorId).SpecializationId == specializationId))
                .Skip(skipRecords)
                .Take(pageSize).Select(m=>new PatientAppointment(m)).ToList();
            Appointments.AddRange(appointments);
            return appointments;

        }

    }
}