using Health.Domain.Abstract;
using Health.Domain.Concrete;
using Health.Domain.Entities;
using Health.WebUI.Infrastructure;
using Health.WebUI.Models.DoctorProfile;
using Health.WebUI.Models.NewPatients;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;

namespace Health.WebUI.Controllers
{
    [Authorize(Roles = "Doctors")]
    public class NextPatientsController : Controller
    {
        IUnitOfWork unitOfWork;
        IPictureManipulator pictureManipulator;
        private int pageSize = 9;
        public NextPatientsController(IUnitOfWork _unitOfWork, IPictureManipulator _pictureManipulator)
        {
            pictureManipulator = _pictureManipulator;
            unitOfWork = _unitOfWork;
        }
        // GET: NextPatients
        public ActionResult Index()
        {
            List<DoctorProfileAppointment> appointments = GetListOfAppointments(0);
            return View(appointments);
        }
        public ActionResult GetAppointments(int page = 0)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Shared/NextPatientsPartialViews/NextDoctorAppointmentsPartialView.cshtml", GetListOfAppointments(page));
            }
            else
            {
                return PartialView("~/Views/Shared/NextPatientsPartialViews/NextDoctorAppointmentsPartialView.cshtml", new EmptyResult());
            }
        }
        public List<DoctorProfileAppointment> GetListOfAppointments(int page)
        {
            List<Appointment> appointments = new AppointmentManipulator(unitOfWork).SkipAppointments(p => p.DoctorId ==
            unitOfWork.Doctors.FindById(HttpContext.User.Identity.GetUserId<int>()).Id
            && p.AppointmentDateTime < DateTime.Now, pageSize, page);
            List<DoctorProfileAppointment> doctorProfileAppointments = new List<DoctorProfileAppointment>();
            foreach (var x in appointments)
            {
                doctorProfileAppointments.Add(new DoctorProfileAppointment(unitOfWork, x.AppointmentId));
            }
            return doctorProfileAppointments;
        }
        public ActionResult GetPatient(int patientId, int appointmentId)
        {
            if (Request.IsAjaxRequest())
            {
                AboutPatientAppoitmentViewModel aboutPatientAppoitmentVM = new AboutPatientAppoitmentViewModel()
                {
                    Id = patientId,
                    Name = unitOfWork.Patients.FindById(patientId).Name,
                    Surname = unitOfWork.Patients.FindById(patientId).Surname,
                    Patronymic = unitOfWork.Patients.FindById(patientId).Patronymic,
                    PatientWeight = unitOfWork.Patients.FindById(patientId).PatientWeight,
                    PatientHeight = unitOfWork.Patients.FindById(patientId).PatientHeight,
                    Age = GetAge(unitOfWork.Patients.FindById(patientId).PatientBirthdate),
                    BloodTypeTitle = unitOfWork.BloodTypes.FindById((int)unitOfWork.Patients.FindById(patientId).BloodTypeId).BloodTypeTitle,
                    Comment = unitOfWork.Appointments.FindById(appointmentId).AppointmentComment,
                    DateTime = unitOfWork.Appointments.FindById(appointmentId).AppointmentDateTime

                };
                return PartialView("~/Views/Shared/NextPatientsPartialViews/PatientDataPartialView.cshtml", aboutPatientAppoitmentVM);
            }
            else
            {
                return new EmptyResult();
            }
        }
        public int GetAge(DateTime dateTime)
        {
            DateTime nowDate = DateTime.Today;
            int age = nowDate.Year - dateTime.Year;
            if (dateTime > nowDate.AddYears(-age)) age--;
            return age;
        }
        public FileContentResult GetPatientPhoto(int? id)
        {
            if (Request.IsAjaxRequest())
            {
                return pictureManipulator.GetPatientPhoto((int)id);
            }
            else
            {
                return null;
            }
        }
        public FileContentResult GetDoctorPhoto(int? id)
        {
            if (Request.IsAjaxRequest())
            {
                return pictureManipulator.GetDoctorPhoto((int)id);
            }
            else
            {
                return null;
            }
        }
    }

}
