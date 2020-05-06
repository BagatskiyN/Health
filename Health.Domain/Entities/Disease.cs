using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Entities
{
   public class Disease
    {
        public int DiseaseId { get; set; }
        public string DiseaseTitle { get; set; }

        public ICollection<Diagnosis> Diagnoses { get; set; }
        
        public Disease()
        {
            Diagnoses = new List<Diagnosis>();

        }


        
    }
}
