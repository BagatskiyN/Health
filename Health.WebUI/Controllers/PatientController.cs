using Health.Domain.Abstract;
using Health.Domain.Entities;
using Health.WebUI.Models;
using Health.WebUI.Models.PatientModels;
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
    public class PatientController : Controller
    {

        UnitOfWork unitOfWork;
        PatientPage patientPage;
        private int pageNex=0;

        public PatientController()
        {

            patientPage = new PatientPage(2);
            unitOfWork = new UnitOfWork();
        }
        public ActionResult Index(int? id)
        {

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


            if (patient != null && patient.PatientImageData!=null&&patient.PatientImageMimeType!=null)
            {
                return File(patient.PatientImageData, patient.PatientImageMimeType);
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
        public ActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                patientPage.Patient.PatientName = patient.PatientName;
                patientPage.Patient.PatientSurname = patient.PatientSurname;
                patientPage.Patient.PatientPatronymic = patient.PatientPatronymic;
                patientPage.Patient.PatientWeight = patient.PatientWeight;
                patientPage.Patient.PatientHeight = patient.PatientHeight;
                patientPage.Patient.PatientBirthdate = patient.PatientBirthdate;
                unitOfWork.Patients.Update(patientPage.Patient);
                unitOfWork.Save();
                TempData["message"] = string.Format("Изменения в пациенте\"{0}\" были сохранены", patient.PatientName);
                return RedirectToAction("Index");
            }
            else
            {
                return View(patient);
            }
        }
     
        
    }
}