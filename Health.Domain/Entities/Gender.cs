using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Entities
{
   public class Gender
    {public int GenderId { get; set; }
        public string GenderTitle { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public Gender()
        {
           Patients = new List<Patient>();
           Doctors = new List<Doctor>();
        }
    }
}
