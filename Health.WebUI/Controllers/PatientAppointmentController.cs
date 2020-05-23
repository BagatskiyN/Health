using Health.Domain.Entities;
using Health.WebUI.App_Start;
using Health.WebUI.Models;
using Health.WebUI.Models.PatientAppointmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Health.WebUI.Controllers
{
    public class PatientAppointmentController : Controller
    {
        // GET: PatientAppointment
        UnitOfWork unitOfWork;
        DoctorListViewModel DoctorListViewModel;
       public static NewAppointmentViewModel newAppointmentViewModel;
        public PatientAppointmentController()
        {
            unitOfWork = new UnitOfWork();
            DoctorListViewModel = new DoctorListViewModel();
          

        }
        public ActionResult Index()
        {
         
            return View(DoctorListViewModel);
        }
        public ActionResult GetDoctorsAppointmentsList(int? id)
        {
            int page = id ?? 0;
            if (Request.IsAjaxRequest())
            {
                DoctorListViewModel.GetPatientAppointmentDoctors(page);

                return PartialView("~/Views/Shared/PatientAppointmentPartialViews/DoctorListView.cshtml",DoctorListViewModel);
            }
            return PartialView("~/Views/Shared/PatientAppointmentPartialViews/DoctorListView.cshtml", DoctorListViewModel);
        }
        public ActionResult MakeNewAppointment(int doctorId)
        {
            int patientId = 2;
            newAppointmentViewModel = new NewAppointmentViewModel(doctorId, (int)patientId);
            SelectList days = new SelectList(newAppointmentViewModel.daysOfWeek.ToList(), "Number", "DayTitle", 1);
            ViewBag.Days = days;
            SelectList hours = new SelectList(newAppointmentViewModel.daysOfWeek[1].Times);
            ViewBag.Hours = hours;
            return View(newAppointmentViewModel);

        }
        [HttpPost]
        public ActionResult MakeNewAppointment(string Day,DateTime Hour,string comment)
        {
            if (ModelState.IsValid)
            {
     
                newAppointmentViewModel.Appointment.AppointmentComment = comment;
                newAppointmentViewModel.Appointment.AppointmentDateTime = Hour;
                Appointment appointment = newAppointmentViewModel.Appointment;
                unitOfWork.Appointments.Create(appointment);
                unitOfWork.Save();
            }
            return RedirectToAction("Index");
        }
        public FileContentResult GetDoctorImage(int doctorId)
        {
            Doctor doctor = unitOfWork.Doctors.FindById(doctorId);


            if (doctor!= null&& doctor.DoctorImageData!=null&&doctor.DoctorImageMimeType!=null)
            {
                return File(doctor.DoctorImageData, doctor.DoctorImageMimeType);
            }
            else
            {
                return null;
            }
        }
        public ActionResult GetItems(int id)
        {   List<DateTime> dateTimes = newAppointmentViewModel.daysOfWeek.Where(p => p.Number == id).FirstOrDefault().Times;
            if (Request.IsAjaxRequest())
            {
             
                return PartialView("~/Views/Shared/PatientAppointmentPartialViews/TimesDropDownPartialView.cshtml", dateTimes);
            }
            return PartialView("~/Views/Shared/PatientAppointmentPartialViews/TimesDropDownPartialView.cshtml", dateTimes);

        }

    }
}