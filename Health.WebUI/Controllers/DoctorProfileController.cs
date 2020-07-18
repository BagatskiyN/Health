using Health.Domain.Abstract;
using Health.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health.WebUI.Controllers
{  
    [Authorize(Roles = "Doctors")]
    public class DoctorProfileController : Controller
    {
        IUnitOfWork unitOfWork;
        DoctorPageViewModel DoctorPageViewModel;



     
        public DoctorProfileController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
           


        }
        [Authorize(Roles = "Doctors")]
        public void CreateDoctorProfilePage()
        {
  DoctorPageViewModel = new DoctorPageViewModel(unitOfWork, User.Identity.GetUserId<int>());
        }

        public FileContentResult GetPatientImage(int? patientId)
        {

            Patient patient = unitOfWork.Patients.FindById((int)patientId);


            if (patient != null && patient.ImageData != null && patient.ImageMimeType != null)
            {
                return File(patient.ImageData, patient.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
        public FileContentResult GetDoctorImage(int doctorId)
        {
            Doctor doctor = unitOfWork.Doctors.FindById(doctorId);


            if (doctor != null && doctor.ImageData != null && doctor.ImageMimeType != null)
            {
                return File(doctor.ImageData, doctor.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

            



        // GET: DoctorProfile
        [Authorize(Roles = "Doctors")]
        public ActionResult Index()
        {
            CreateDoctorProfilePage();
            return View(DoctorPageViewModel);
        }

        [Authorize(Roles = "Doctors")]
        public ActionResult OtherAction()
        {
            return View("Index", GetData("OtherAction"));
        }

        public ActionResult GetPreviousAppointments(int page)
        {
               if (Request.IsAjaxRequest())
            {
                return PartialView("", DoctorPageViewModel.GetPreviousAppointmentsList(page));
            }
            return new EmptyResult();
               
        }

        private Dictionary<string, object> GetData(string actionName)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("Action", actionName);
            dict.Add("Пользователь", HttpContext.User.Identity.Name);
            dict.Add("Аутентифицирован?", HttpContext.User.Identity.IsAuthenticated);
            dict.Add("Тип аутентификации", HttpContext.User.Identity.AuthenticationType);
            dict.Add("В роли Doctors?", HttpContext.User.IsInRole("Doctors"));

            return dict;
        }
    }
}