using Health.Domain.Abstract;
using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Concrete
{
   public class EFPatientRepository : IRepository<Patient>
    {
        EFDbContext context = new EFDbContext();
     
        public void Create(Patient item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Patient GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Patient> GetItemList
        {
           get { return context.Patients; }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Patient item)
        {
            throw new NotImplementedException();
        }
    }
}
