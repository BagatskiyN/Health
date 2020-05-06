using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Entities
{
   public class Doctor
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }

        public string DoctorPatronymic { get; set; }
        public string DoctorPhone { get; set; }

        public string DoctorEmail { get; set; }

        public byte[] DoctorImageData { get; set; }
        public string DoctorImageMimeType { get; set; }
        public int? SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }
       
        public ICollection<Appointment> Appointments { get; set; }
       public int GenderId { get; set; }
        public virtual Gender Gender { get; set; }
        public Doctor()
        {
         Appointments = new List<Appointment>();
      
        }


    }
}
