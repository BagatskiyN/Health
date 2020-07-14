using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Health.Domain.Entities
{
   public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [HiddenInput(DisplayValue = false)]
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int? GenderId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual Gender Gender { get; set; }
    
        public string DoctorPhone { get; set; }

        public string DoctorEmail { get; set; }

        public int? SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }
       
        public ICollection<Appointment> Appointments { get; set; }
  
        public Doctor()
        {
         Appointments = new List<Appointment>();
      
        }


    }
}
