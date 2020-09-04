using Health.Domain.Abstract;
using Health.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health.WebUI.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class AdminSpecializationsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public AdminSpecializationsController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        // GET: AdminSpecializations
        public ActionResult Index()
        {
            List<Specialization> specializations = unitOfWork.Specializations.Get().OrderBy(n=>n.SpecializationTitle).ToList();
            return View(specializations);
        }

        public ActionResult EditSpecialization(int id)
        {
            Specialization specializationEdit = unitOfWork.Specializations.FindById(id); 
            return View(specializationEdit);
        }
        [HttpPost]
        public ActionResult EditSpecialization(Specialization specialization, HttpPostedFileBase imageInp)
        {
            if(ModelState.IsValid)
            {
                if(specialization!=null)
                {
                    Specialization specializationEdit = unitOfWork.Specializations.FindById(specialization.SpecializationId);
                    specializationEdit.SpecializationTitle = specialization.SpecializationTitle;
                    specializationEdit.SpecializationTitle = specialization.SpecializationTitle;
                    if (imageInp != null)
                    {
                        specializationEdit.SpecializationImageMimeType = imageInp.ContentType;
                        specializationEdit.SpecializationImageData = new byte[imageInp.ContentLength];
                        imageInp.InputStream.Read(specializationEdit.SpecializationImageData, 0, imageInp.ContentLength);
                    }
                    unitOfWork.Specializations.Update(specializationEdit);
                }
            }
            else
            {
                ModelState.AddModelError("", "Специальность не найдена");
            }
            return RedirectToAction("Index","AdminSpecializations");
        }
        public ActionResult CreateSpecialization()
        {
            return View();
        }
        [HttpPost] 
        public ActionResult CreateSpecialization(string specializationTitle, HttpPostedFileBase imageInp)
        {
            if (string.IsNullOrEmpty(specializationTitle))
            {
                ModelState.AddModelError("SpecializationTitle", "Некорректное название специализации");
            }     
            if (ModelState.IsValid)
            {
                if (specializationTitle != null)
                {
                    Specialization specialization = new Specialization()
                    {
                        SpecializationTitle= specializationTitle
                    };

                    if (imageInp != null)
                    {
                        specialization.SpecializationImageMimeType = imageInp.ContentType;
                        specialization.SpecializationImageData = new byte[imageInp.ContentLength];
                        imageInp.InputStream.Read(specialization.SpecializationImageData, 0, imageInp.ContentLength);  
                    
                    }   
                    unitOfWork.Specializations.Create(specialization);
                }
            }

            return RedirectToAction("Index", "AdminSpecializations");
        }
        [HttpPost]

        public ActionResult Delete(int id)
        {
            Specialization specialization = unitOfWork.Specializations.FindById(id);
            if(specialization!=null)
            {
                unitOfWork.Specializations.Remove(specialization);
            }

            return RedirectToAction("Index");
        }

    }
}