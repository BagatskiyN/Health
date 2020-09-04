using Health.Domain.Abstract;
using Health.Domain.Entities;
using Health.WebUI.Models.AdminDisease;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health.WebUI.Controllers
{
    public class AdminDiseasesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminDiseasesController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        // GET: AdminDiseases
        public ActionResult Index()
        {
            List<Disease> diseases = unitOfWork.Diseases.Get().ToList();
            return View(diseases);
        }
        public ActionResult CreateDisease()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDisease(Disease disease)
        {
            if(ModelState.IsValid)
            {
                Disease Newdisease = new Disease()
                {
                    DiseaseTitle = disease.DiseaseTitle
                };
                unitOfWork.Diseases.Create(disease);
            }
            return RedirectToAction("Index", "AdminDiseases");
        }

        public ActionResult EditDisease(int id)
        {
            Disease disease = unitOfWork.Diseases.FindById(id);
            return View(disease);
        }

        [HttpPost]
        public ActionResult EditDisease(Disease disease)
        {
            if(ModelState.IsValid)
            {
                Disease diseaseUpdate = unitOfWork.Diseases.FindById(disease.DiseaseId);
                diseaseUpdate.DiseaseTitle = disease.DiseaseTitle;
                unitOfWork.Diseases.Update(diseaseUpdate);

            }
            return RedirectToAction("Index", "AdminDiseases");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Disease disease = unitOfWork.Diseases.FindById(id);
            if (disease != null)
            {
                unitOfWork.Diseases.Remove(disease);
            }

            return RedirectToAction("Index");
        }
    }
}