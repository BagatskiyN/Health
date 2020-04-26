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
        public ICollection<Diagnosis> Diagnoses { get; set; }
        public ICollection<UpcomingAppointment> UpcomingAppointments { get; set; }
        public ICollection<PreviousAppointment> PreviousAppointments { get; set; }
        public Doctor()
        {
            Diagnoses = new List<Diagnosis>();
            UpcomingAppointments = new List<UpcomingAppointment>();
            PreviousAppointments = new List<PreviousAppointment>();
        }


    }
}
