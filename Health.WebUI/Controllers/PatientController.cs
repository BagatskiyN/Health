using Health.Domain.Abstract;
using Health.Domain.Entities;
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
        public PatientController(IRepository<Patient> repos,IRepository<Appointment> _repositoryAppointment,IRepository<Doctor> _repositoryDoctor)
        {
            repository = repos;
            repositoryAppointment = _repositoryAppointment;
            repositoryDoctor = _repositoryDoctor;
  
        }
        public ActionResult Index()
        {
            return View(repository.GetItem(2));
            
        }

        [ChildActionOnly]
       public ActionResult AppointmentList(object id)
        {
            return PartialView("AppointmentList.cshtml",repositoryAppointment.GetItemList.Where(p=>p.PatientId==(int)id));
        
        }

    }
}