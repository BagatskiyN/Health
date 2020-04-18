using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Health.Domain.Entities;

namespace Health.Domain.Concrete
{
    class EFDbContext:DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
    }
}
