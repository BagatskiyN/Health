using Health.Domain.Abstract;
using Health.Domain.Entities;
using Health.WebUI.Models;
using Health.WebUI.Models.PatientModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Windows.Media.Imaging;

namespace Health.WebUI.Controllers
{
    [Authorize(Roles = "Patients")]
    public class PatientController : Controller
    {

        IUnitOfWork unitOfWork;
        PatientPage patientPage;
        private int pageNex=0;

        public PatientController(IUnitOfWork _unitOfWork)
        {
            unitOfWork =_unitOfWork;
        }


        public PatientPage GetPatientPage()
        {
           return new PatientPage(User.Identity.GetUserId<int>());
            
        }       
        
        [Authorize(Roles = "Patients")]
        public ActionResult Index(int? id)
        {
            patientPage = GetPatientPage();
            return View(patientPage);

        }

        public ActionResult GetNextAppointmentsList(int? id)
        {
            int page = id ?? 0;
            pageNex = page;
            if (Request.IsAjaxRequest())
            {
                patientPage.GetNextPatientAppointmentsList(page);

                return PartialView("~/Views/Shared/PatientPartialViews/NextAppointmentList.cshtml", patientPage);
            }
            return PartialView("~/Views/Shared/PatientPartialViews/NextAppointmentList.cshtml", patientPage);
        }
        public ActionResult DeleteAppointment(int? AppointmentId)
        {
            int appointmentId = AppointmentId ?? 0;
            Appointment appointment = unitOfWork.Appointments.FindById(appointmentId);
            unitOfWork.Appointments.Remove(appointment);
            return RedirectToAction("Index");
            //if (Request.IsAjaxRequest())
            //{
            //    patientPage.GetNextPatientAppointmentsList(pageNex);
            //    return PartialView("~/Views/Shared/PatientPartialViews/NextAppointmentList.cshtml", patientPage);
            //}
            //return PartialView("~/Views/Shared/PatientPartialViews/NextAppointmentList.cshtml", patientPage);


        }


        public ActionResult GetPreviousAppointmentsList(int? id)
        {
            int page = id ?? 0;
            if (Request.IsAjaxRequest())
            {
                patientPage.GetPreviousPatientAppointmentsList(page);

                return PartialView("~/Views/Shared/PatientPartialViews/PreviousAppointmentList.cshtml", patientPage);
            }
            return PartialView("~/Views/Shared/PatientPartialViews/PreviousAppointmentList.cshtml", patientPage);
        }
        public FileContentResult GetSpacializationImage(int specializationId)
        {
            Specialization specialization = unitOfWork.Specializations.FindById(specializationId);


            if (specialization != null)
            {
                return File(specialization.SpecializationImageData, specialization.SpecializationImageMimeType);
            }
            else
            {
                return null;
            }
        }
        public FileContentResult GetPatientImage(int patientId)
        {
            Patient patient = unitOfWork.Patients.FindById(patientId);


            if (patient != null && patient.ImageData!=null&&patient.ImageMimeType!=null)
            {
                return File(patient.ImageData, patient.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

            public ViewResult Edit(int patientId)
        {
            Patient patient = unitOfWork.Patients.FindById(patientId);
            return View(patient);

        }
        [HttpPost]
        public ActionResult Edit(Patient patient, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                patientPage.Patient.Name = patient.Name;
                patientPage.Patient.Surname = patient.Surname;
                patientPage.Patient.Patronymic = patient.Patronymic;
                patientPage.Patient.PatientWeight = patient.PatientWeight;
                patientPage.Patient.PatientHeight = patient.PatientHeight;
                patientPage.Patient.PatientBirthdate = patient.PatientBirthdate;
                unitOfWork.Patients.Update(patientPage.Patient);
                unitOfWork.Save();
                TempData["message"] = string.Format("Изменения в пациенте\"{0}\" были сохранены", patient.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(patient);
            }
        }
     
        
    }
}