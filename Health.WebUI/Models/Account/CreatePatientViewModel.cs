using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models.Account
{
    public class CreatePatientViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Patronymic { get; set; }
        
        [Required]
        public DateTime BirthDate { get; set; }

        

        public int?  BloodTypeId { get; set; }

       

        public int? GenderId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}