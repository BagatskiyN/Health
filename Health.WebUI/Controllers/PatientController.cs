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
        public PatientController(IRepository<Patient> repos)
        {
            repository = repos;
        }
        public ActionResult Index()
        {
            return View();
        }


    }
}