using Health.Domain.Abstract;
using Health.Domain.Entities;
using Health.WebUI.App_Start;
using Health.WebUI.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Health.WebUI.Models.PatientModels
{
    public class PatientPage : IMakeElement<PatientPage>
    {
        UnitOfWork unitOfWork;
        const int pageSize = 6;
        const int nextPageSize = 4;
        private int PatientId;
        public Patient Patient { get; set; }
        public int PatientAge { get; set; }

        public Gender PatientGender { get; set; }
        public BloodType PatientBloodType { get; set; }
        public List<PatientAppointment> PreviousAppointments { get; set; } = new List<PatientAppointment>();
        public List<PatientAppointment> NextAppointments { get; set; } = new List<PatientAppointment>();


        public PatientPage(int patientId)
        {
            unitOfWork = new UnitOfWork();
            PatientId = patientId;
           Patient= unitOfWork.Patients.FindById(PatientId);
            if (Patient.GenderId.HasValue)
                PatientGender = unitOfWork.Genders.FindById((int)Patient.GenderId);
            if (Patient.BloodTypeId.HasValue)
                PatientBloodType = unitOfWork.BloodTypes.FindById((int)Patient.BloodTypeId);
          
            if(unitOfWork.Appointments.Get().Where(p=>p.PatientId==PatientId)!=null)
            {
              PreviousAppointments= GetPreviousPatientAppointmentsList();
                NextAppointments = GetNextPatientAppointmentsList();
            }
            DateTime nowDate = DateTime.Today;
            int age = nowDate.Year - Patient.PatientBirthdate.Year;
            if (Patient.PatientBirthdate > nowDate.AddYears(-age)) age--;
            PatientAge = age;
           

        }
        public List<PatientAppointment> GetPreviousPatientAppointmentsList(int page = 0)
        {
            List<PatientAppointment> result = new List<PatientAppointment>();
            List<Appointment> appointments = GetPreviousAppointmentsList(page);
            for (int i = 0; i < appointments.Count; i++)
            {
              result.Add(new PatientAppointment(appointments[i]));
            }

            PreviousAppointments = result;
            return result;

        }
        public List<PatientAppointment> GetNextPatientAppointmentsList(int page = 0)//Сделать похожей на previous
        {
            List<PatientAppointment> result = new List<PatientAppointment>();
            List<Appointment> appointments = GetNextAppointmentsList(page);
            for (int i = 0; i < appointments.Count; i++)
            {
                result.Add(new PatientAppointment(appointments[i]));
            }
            NextAppointments = result;
            return result;

        }
       
        private List<Appointment> GetPreviousAppointmentsList(int page = 0)
        {

            List<Appointment> appointments = unitOfWork.Appointments.Get()
                .Where(p => p.PatientId == PatientId && p.AppointmentDateTime < DateTime.Now).ToList();
            return SkipUsedAppointments(appointments, page);
        }
        private List<Appointment> GetNextAppointmentsList(int page = 0)
        {

            List<Appointment> appointments = unitOfWork.Appointments.Get()
                .Where(p => p.PatientId == PatientId && p.AppointmentDateTime > DateTime.Now).ToList();
            return SkipUsedNextAppointments(appointments, page);

        }

        private List<Appointment> SkipUsedAppointments(List<Appointment> appointments, int page)
        {
            var skipRecords = page * pageSize;
            if (appointments.Count() - skipRecords >= 0)
            {
                if (appointments.Count() - skipRecords < 6)
                {
                    return appointments.OrderByDescending(t => t.AppointmentDateTime)
                           .Skip(skipRecords).Take(appointments.Count() - skipRecords)
                           .ToList();
                }
                else
                {
                    return appointments.OrderByDescending(t => t.AppointmentDateTime)
                      .Skip(skipRecords).Take(pageSize).ToList();
                }
            }
            else
            {
                return new List<Appointment> { };
            }
        }
        
        private List<Appointment> SkipUsedNextAppointments(List<Appointment> appointments,int page)
        {
            var skipRecords = page * nextPageSize;
            if (appointments.Count() - skipRecords >= 0)
            {
                if (appointments.Count() - skipRecords < 4)
                {
                    return appointments.OrderBy(t => t.AppointmentDateTime)
                           .Skip(skipRecords).Take(appointments.Count() - skipRecords)
                           .ToList();
                }
                else
                {
                    return appointments.OrderBy(t => t.AppointmentDateTime)
                      .Skip(skipRecords).Take(nextPageSize).ToList();
                }
            }
            else
            {
                return new List<Appointment> { };
            }
        }

        public PatientPage MakeElement(int patientId)
        {
            return null;
        }


    }
}