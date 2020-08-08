using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models.NewPatients
{
    public class AboutPatientAppoitmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string BloodTypeTitle { get; set; }

        public int Age { get; set; }

        public int PatientWeight { get; set; }

        public int PatientHeight { get; set; }
        
        public int AppointmentId { get; set; } 

        public string Comment { get; set; }

        public DateTime DateTime { get; set; }
    }
}