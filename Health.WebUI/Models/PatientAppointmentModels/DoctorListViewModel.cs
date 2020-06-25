using Health.Domain.Abstract;
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
        public IUnitOfWork unitOfWork;
        public DoctorListViewModel(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            PatientAppointmentDoctors = GetPatientAppointmentDoctors();
        }
        public  List<PatientAppointmentDoctor> GetPatientAppointmentDoctorsSearch(string searchText, string filterSpecialization)
        {
            Specialization specialization;
          
            List<Doctor> patientDoctorsFilter = new List<Doctor>();
            List<Doctor> patientDoctorsSearch = new List<Doctor>();
         

            if (filterSpecialization != "")
            {
                specialization = unitOfWork.Specializations.Get().FirstOrDefault(x => x.SpecializationTitle == filterSpecialization);
                patientDoctorsFilter = unitOfWork.Doctors.Get().Where(x => x.SpecializationId == specialization.SpecializationId).ToList();
            }
            if (searchText != "")
            {
                patientDoctorsSearch = unitOfWork.Doctors.Get().Where(x => (x.DoctorName.Contains(searchText))).ToList();

            }

            if (patientDoctorsSearch.Count != 0 && patientDoctorsFilter.Count != 0)
            {
                var result= patientDoctorsFilter.Select(x=>x.DoctorId).Intersect(patientDoctorsSearch.Select(x=>x.DoctorId)).Select(y => new PatientAppointmentDoctor(y, unitOfWork)).ToList();
                PatientAppointmentDoctors = result;
                return result;
            }
            else
            {
                if (patientDoctorsSearch.Count != 0)
                {
                    var result = patientDoctorsSearch.Select(y => new PatientAppointmentDoctor(y.DoctorId,unitOfWork)).ToList();
                    PatientAppointmentDoctors = result;
                    return result;
                }
                else
                {
                    var result = patientDoctorsFilter.Select(y => new PatientAppointmentDoctor(y.DoctorId, unitOfWork)).ToList();
                    return result;
                }
            }
        }

        public List<PatientAppointmentDoctor> GetPatientAppointmentDoctors(int page = 0)
        {
            List<Doctor> doctors = unitOfWork.Doctors.Get().Skip(page * pageSize).Take(pageSize).ToList();
            List<PatientAppointmentDoctor> result = new List<PatientAppointmentDoctor>();
            for (int i = 0; i < doctors.Count(); i++)
            {
                result.Add(new PatientAppointmentDoctor(doctors[i].DoctorId, unitOfWork));
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