using Health.Domain.Abstract;
using Health.Domain.Entities;
using Health.WebUI.Models.DoctorProfile;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Runtime.InteropServices;
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

        public FileContentResult GetPatientImage(int patientId)
        {

            Patient patient = unitOfWork.Patients.FindById(patientId);


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
                throw new Exception("Картинка не найдена"); 
            }
        }
        public ActionResult EditDiagnosis(DiagnosisViewModel diagnosisViewModel)
        {
            SelectList selectListDiseases = new SelectList(unitOfWork.Diseases.Get().ToList());

        
            ViewBag.Disease = unitOfWork.Diseases.Get().ToList();
            return View(diagnosisViewModel);
        }
        [HttpPost]
        public ActionResult SaveEditedDiagnosis(DiagnosisViewModel diagnosisViewModel)
        {
            if (ModelState.IsValid)
            {
                Diagnosis diagnosis = unitOfWork.Diagnoses.FindById(diagnosisViewModel.DiagnosisId);
                if (diagnosis != null)
                {
                    diagnosis.DiagnosisComment = diagnosisViewModel.DiagnosisComment;
                    diagnosis.DiagnosisRecommendations = diagnosisViewModel.DiagnosisRecommendations;
                    diagnosis.DiseaseId = diagnosisViewModel.DiseaseId;
                    unitOfWork.Diagnoses.Update(diagnosis);
                }
                else
                {
                    Disease disease = unitOfWork.Diseases.FindById((int)diagnosisViewModel.DiseaseId);

                    Diagnosis NewDiagnosis = new Diagnosis
                    {
                        DiseaseId = disease.DiseaseId,
                        DiagnosisId = diagnosisViewModel.DiagnosisId,
                        DiagnosisComment = diagnosisViewModel.DiagnosisComment,
                        DiagnosisRecommendations=diagnosisViewModel.DiagnosisRecommendations

                    };
                    unitOfWork.Diagnoses.Create(NewDiagnosis);
                }
                return RedirectToAction("GetDiagnosis",new {diagnosisId= diagnosisViewModel.DiagnosisId });
            }
            else
            {
                return View("Error", new string[] { "Диагноз не удалось сохранить" });
            }
        }

        public JsonResult GetDiseasesBySearch(string searchText)
        {
            List<Disease> DiseaseList = new List<Disease>();
            if(searchText!=null)
            {
                     DiseaseList = unitOfWork.Diseases.Get()
                    .Where(x => x.DiseaseTitle.ToLower()
                    .Contains(searchText.ToLower())).ToList();
            }
            else
            {
                DiseaseList = unitOfWork.Diseases.Get().ToList();
            }
      
            return Json(DiseaseList, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetDiagnosis(int diagnosisId)
        {
            Diagnosis diagnosis = unitOfWork.Diagnoses.FindById(diagnosisId);
            if (diagnosis == null)
            {
                diagnosis = new Diagnosis()
                {
                   DiagnosisId = diagnosisId,
                DiagnosisComment = "Комментария нет",
                DiagnosisRecommendations = "Рекомендаций нет"
                
                };
                Disease disease= new Disease() { DiseaseTitle = "Нет диагноза" };
                diagnosis.Disease = disease;
                DiagnosisViewModel diagnosisViewModel = new DiagnosisViewModel()
                {
                    DiagnosisId = diagnosis.DiagnosisId,
                    DiagnosisComment = diagnosis.DiagnosisComment,
                    DiagnosisRecommendations = diagnosis.DiagnosisRecommendations,
                    DiseaseId = diagnosis.DiseaseId,
                    DiseaseTitle = disease.DiseaseTitle
            };

            return View(diagnosisViewModel);
            }
            else
            {
                DiagnosisViewModel diagnosisViewModel = new DiagnosisViewModel()
                {
                    DiagnosisId = diagnosis.DiagnosisId,
                    DiagnosisComment = diagnosis.DiagnosisComment,
                    DiagnosisRecommendations = diagnosis.DiagnosisRecommendations,
                    DiseaseId = diagnosis.DiseaseId,
                    DiseaseTitle = unitOfWork.Diseases.FindById((int)diagnosis.DiseaseId).DiseaseTitle,
                };
                return View(diagnosisViewModel);
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
            dict.Add("Пользователь", HttpContext.User.Identity.GetUserId());
            dict.Add("Аутентифицирован?", HttpContext.User.Identity.IsAuthenticated);
            dict.Add("Тип аутентификации", HttpContext.User.Identity.AuthenticationType);
            dict.Add("В роли Doctors?", HttpContext.User.IsInRole("Doctors"));

            return dict;
        }
    }
}