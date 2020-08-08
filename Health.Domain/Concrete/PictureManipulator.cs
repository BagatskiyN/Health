using Health.Domain.Abstract;
using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Health.WebUI.Infrastructure
{
    public class PictureManipulator: Controller, IPictureManipulator
    {
        IUnitOfWork unitOfWork;
        public PictureManipulator(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;

        }
      
        public FileContentResult GetDoctorPhoto(int id)
        {

            Doctor doctor = unitOfWork.Doctors.FindById(id);


            if (doctor != null && doctor.ImageData != null && doctor.ImageMimeType != null)
            {
                return File(doctor.ImageData, doctor.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
        public FileContentResult GetPatientPhoto(int id)
        {

            Patient patient = unitOfWork.Patients.FindById(id);


            if (patient != null && patient.ImageData != null && patient.ImageMimeType != null)
            {
                return File(patient.ImageData, patient.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
        public FileContentResult GetSpecializationIcon(int id)
        {
            Patient patient = unitOfWork.Patients.FindById(id);


            if (patient != null && patient.ImageData != null && patient.ImageMimeType != null)
            {
                return File(patient.ImageData, patient.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
     
    }  
   
}