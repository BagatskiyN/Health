using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Entities
{
   public class Specialization
    {
        public int SpecializationId { get; set; }
        public string SpecializationTitle { get; set; }
        public byte[] SpecializationImageData { get; set; }
        public string SpecializationImageMimeType { get; set; }

        public virtual ICollection<Specialization> Specializations { get; set; }
        public Specialization()
        {
            Specializations = new List<Specialization>();
        }
    }
}
