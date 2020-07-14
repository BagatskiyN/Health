using Health.Domain.Abstract;
using Health.Domain.Concrete;
using Health.Domain.Entities;
using Health.WebUI.App_Start;
using Health.WebUI.Models;
using Health.WebUI.Models.PatientAppointmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Health.WebUI.Controllers
{
    public class PatientAppointmentController : Controller
    {
        // GET: PatientAppointment
       public IUnitOfWork unitOfWork;
      public  DoctorListViewModel DoctorListViewModel;
       public static NewAppointmentViewModel newAppointmentViewModel;
        public PatientAppointmentController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            DoctorListViewModel = new DoctorListViewModel(unitOfWork);

        }
        public ActionResult Index()
        {
           SelectList selectLists=new SelectList(unitOfWork.Specializations.Get().Select(x => x.SpecializationTitle));
          
            ViewBag.Specializations = selectLists;
            DoctorListViewModel.GetPatientAppointmentDoctors();
            return View(DoctorListViewModel);
        }
        public ActionResult GetDoctorsAppointmentsList(int? id)
        {
            int page = id ?? 0;
            if (Request.IsAjaxRequest())
            {
                DoctorListViewModel.GetPatientAppointmentDoctors(page);

                return PartialView("~/Views/Shared/PatientAppointmentPartialViews/DoctorListView.cshtml",DoctorListViewModel.PatientAppointmentDoctors);
            }
            return PartialView("~/Views/Shared/PatientAppointmentPartialViews/DoctorListView.cshtml", DoctorListViewModel.PatientAppointmentDoctors);
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
        private string prevSearchStr;
      
        public ActionResult DoctorsSearch(string searchText,string filter)
        {
            if (prevSearchStr != searchText)
            {
            
                prevSearchStr = searchText;
                if (Request.IsAjaxRequest())
                {   
                    if(searchText==""&&(filter=="Выберите специальность"||filter==""))
                {
                    DoctorListViewModel.GetPatientAppointmentDoctors(0);
                    return PartialView("~/Views/Shared/PatientAppointmentPartialViews/DoctorListView.cshtml", DoctorListViewModel.PatientAppointmentDoctors);
                }
                   
                    DoctorListViewModel.GetPatientAppointmentDoctorsSearch(searchText, filter);

                    return PartialView("~/Views/Shared/PatientAppointmentPartialViews/DoctorListView.cshtml", DoctorListViewModel.PatientAppointmentDoctors);
                }
                return PartialView("~/Views/Shared/PatientAppointmentPartialViews/DoctorListView.cshtml", DoctorListViewModel.PatientAppointmentDoctors);
            }
           
            return new EmptyResult();
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


            if (doctor!= null&& doctor.ImageData!=null&&doctor.ImageMimeType!=null)
            {
                return File(doctor.ImageData, doctor.ImageMimeType);
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