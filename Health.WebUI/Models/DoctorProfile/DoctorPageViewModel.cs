using Health.Domain.Abstract;
using Health.Domain.Entities;
using Health.WebUI.Models.DoctorProfile;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class DoctorPageViewModel
{
    int pageSize = 9;
    IUnitOfWork unitOfWork;
    public Doctor Doctor { get; set; }
    public Specialization Specialization { get; set; }

    public List<DoctorProfileAppointment> PreviousAppointment { get; set; }

    public DoctorPageViewModel(IUnitOfWork _unitOfWork, int doctorId)
    {
        unitOfWork = _unitOfWork;
        Doctor = unitOfWork.Doctors.FindById(doctorId);
        if (Doctor != null)
        {
            Specialization = unitOfWork.Specializations.FindById((int)Doctor.SpecializationId);
        }
        PreviousAppointment = GetPreviousAppointmentsList(0);

    }
    public List<DoctorProfileAppointment> GetPreviousAppointmentsList(int page = 1)
    {
       List<Appointment> appointments= unitOfWork.Appointments.Get()
            .Where(p => p.DoctorId == Doctor.Id && p.AppointmentDateTime < DateTime.Now)
            .Skip((page - 1) * pageSize).Take(pageSize).ToList();

        List<DoctorProfileAppointment> doctorProfileAppointments = new List<DoctorProfileAppointment>();
       foreach(var x in appointments)
        {
            doctorProfileAppointments.Add(new DoctorProfileAppointment(unitOfWork, x.AppointmentId));
        }
        return doctorProfileAppointments;

    }

}
