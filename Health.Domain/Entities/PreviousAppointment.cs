using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Entities
{
   public class PreviousAppointment
    {
        public int PreviousAppointmentId { get; set; }

        public DateTime PreviousAppointmentDateTime { get; set; }

        public bool PreviousAppointmentTookPlace { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }
  
       

        public  Diagnosis Diagnosis { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
      
    }
}
