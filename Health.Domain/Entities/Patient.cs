using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Entities
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string PatientPatronymic { get; set; }
        public DateTime PatientBirthdate { get; set; }
       
        public int PatientWeight { get; set; }
        public int PatientHeight { get; set; }
        public byte[] PatientImageData { get; set; }
        public string PatientImageMimeType { get; set; }
        public int? GenderId { get; set; }
        public virtual Gender Gender { get; set; }
        public int? BloodTypeId { get; set; }
        public virtual BloodType BloodType { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
      
        public Patient()
        {
       
          Appointments = new List<Appointment>();
    
        }
   
    

    }
}
