using Health.Domain.Abstract;
using Health.Domain.Entities;
using Health.WebUI.Models.MedicalRecord;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Health.WebUI.Controllers
{
    [Authorize(Roles = "Patients")]
    public class MedicalRecordController : Controller
    {
        // GET: MedicalRecord
      private List<Specialization> specializations;
      public static SpecializationRecordsListViewModel specializationRecordsListViewModel;
        public ActionResult Index()
        {
           specializations = unitOfWork.Specializations.Get().ToList();

            return View(specializations);
        }
       public IUnitOfWork unitOfWork;
        public MedicalRecordController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;


        }  
        static object locker = new object();
        public FileContentResult GetSpecializationIcon(int specializationId)
        {
         
            lock (locker)
            {
                Specialization specialization = unitOfWork.Specializations.FindById(specializationId);
                if (specialization != null && specialization.SpecializationImageData != null && specialization.SpecializationImageMimeType != null)
                {
                    return File(specialization.SpecializationImageData, specialization.SpecializationImageMimeType);
                }
                else
                {
                    return null;
                }
            }

        }
        public ActionResult SpecializationRecords(int specializationId)
        {
            specializationRecordsListViewModel= new SpecializationRecordsListViewModel(unitOfWork,specializationId, User.Identity.GetUserId<int>());
            specializationRecordsListViewModel.GetAppointmentsBySpecialization();
            return View(specializationRecordsListViewModel);
        }
        [HttpGet] 
        public ActionResult GetSpecializationRecords(int page)
        {
            if (Request.IsAjaxRequest())
            {   
                return PartialView("~/Views/Shared/MedicalRecordPartialViews/RecordView.cshtml", 
                    specializationRecordsListViewModel.GetAppointmentsBySpecialization(page));
            }
            return new EmptyResult();
        }
        [HttpGet]
        public ActionResult GetAppointmentData(int appointmentId)
        {
            lock (locker)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/Shared/MedicalRecordPartialViews/AppointmentDataView.cshtml", new AppointmentData(appointmentId,unitOfWork));

                }
                return new EmptyResult();
            }
        }
        public ActionResult SearchSpecialization(string searchText)
        {
            lock (locker)
            {
                if (Request.IsAjaxRequest())
                {if(searchText=="")
                    {
                        specializations = unitOfWork.Specializations.Get().ToList();
                        return PartialView("~/Views/Shared/MedicalRecordPartialViews/MedicalRecordSpecializationListView.cshtml", specializations);
                    }
                    else
                    {   
                    specializations = unitOfWork.Specializations.Get(m => m.SpecializationTitle.Contains(searchText)).ToList();
                    return PartialView("~/Views/Shared/MedicalRecordPartialViews/MedicalRecordSpecializationListView.cshtml", specializations);
                       
                    }
                   
                }
                return PartialView("~/Views/Shared/MedicalRecordPartialViews/MedicalRecordSpecializationListView.cshtml", specializations);
            }
        }

    }
}