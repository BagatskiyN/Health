using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health.Domain.Entities
{
    public class Patient
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
   
        public int? GenderId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual Gender Gender { get; set; }
        public DateTime PatientBirthdate { get; set; }
        [Required]
        [Display(Name = "Вес")]
        public int PatientWeight { get; set; }
        [Required]
        [Display(Name = "Рост")]
        public int PatientHeight { get; set; }

       
        public int? BloodTypeId { get; set; }
        
        [HiddenInput(DisplayValue = false)]
        public virtual BloodType BloodType { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
      
        public Patient()
        {
       
          Appointments = new List<Appointment>();
    
        }
   
    

    }
}
