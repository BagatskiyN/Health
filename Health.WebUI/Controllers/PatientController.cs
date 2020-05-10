using Health.Domain.Abstract;
using Health.Domain.Entities;
using Health.WebUI.Models.PatientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health.WebUI.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        private IRepository<Patient> repository;
        private IRepository<Appointment> repositoryAppointment;
        private IRepository<Doctor> repositoryDoctor;
        PatientPage patientPage;
        public PatientController(IRepository<Patient> repos, IRepository<Appointment> _repositoryAppointment, IRepository<Doctor> _repositoryDoctor)
        {
            repository = repos;
            repositoryAppointment = _repositoryAppointment;
            repositoryDoctor = _repositoryDoctor;
            patientPage = new PatientPage(2);
        }
        public ActionResult Index(int? id)
        {
            int page = id ?? 0;
            if (Request.IsAjaxRequest())
            {
                patientPage.GetPreviousPatientAppointmentsList(page);

                return PartialView("~/Views/Shared/PatientPartialViews/AppointmentList.cshtml", patientPage);
            }
            return View(patientPage);

        }


        //[ChildActionOnly]
        //public ActionResult AppointmentList(object id)
        // {
        //     return PartialView("AppointmentList.cshtml",repositoryAppointment.GetItemList.Where(p=>p.PatientId==(int)id));

        // }


    }
}