using Health.Domain.Entities;
using Health.WebUI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models.PatientAppointmentModels
{
    public class DoctorListViewModel
    {
        int pageSize = 9;
        public List<PatientAppointmentDoctor> PatientAppointmentDoctors { get; set; }
        UnitOfWork unitOfWork;
        public DoctorListViewModel()
        {
            unitOfWork = new UnitOfWork();
           PatientAppointmentDoctors=GetPatientAppointmentDoctors();
        }

        public List<PatientAppointmentDoctor> GetPatientAppointmentDoctors(int page=0)
        {
            List<Doctor> doctors = unitOfWork.Doctors.Get().Skip(page*pageSize).Take(pageSize).ToList();
            List<PatientAppointmentDoctor> result = new List<PatientAppointmentDoctor>();
            for (int i=0;i<doctors.Count();i++)
            {
                result.Add(new PatientAppointmentDoctor(doctors[i].DoctorId));
            }
            PatientAppointmentDoctors = result;
            return result;

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



    }

 
}