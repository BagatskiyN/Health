using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Health.Domain.Entities
{
    public class Patient
    {
        [HiddenInput(DisplayValue = false)]
        public int PatientId { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string PatientName { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string PatientSurname { get; set; }
        [Required]
        [Display(Name = "Отчество")]
        public string PatientPatronymic { get; set; }
        [Required]
        [Display(Name = "День рождения")]
        public DateTime PatientBirthdate { get; set; }
        [Required]
        [Display(Name = "Вес")]
        public int PatientWeight { get; set; }
        [Required]
        [Display(Name = "Рост")]
        public int PatientHeight { get; set; }

        [HiddenInput(DisplayValue = false)]
        public byte[] PatientImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string PatientImageMimeType { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int? GenderId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual Gender Gender { get; set; }
        
        [HiddenInput(DisplayValue = false)]
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
