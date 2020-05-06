using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Entities
{
    public class BloodType
    {
        public int BloodTypeId { get; set; }
        public string BloodTypeTitle{get;set;}
        public ICollection<Patient> Patients { get; set; }
        public BloodType()
        {
            Patients = new List<Patient>();

        }

    }
}
