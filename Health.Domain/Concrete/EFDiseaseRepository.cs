using Health.Domain.Abstract;
using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Concrete
{
    public class EFDiseaseRepository : IRepository<Disease>
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Disease> GetItemList => throw new NotImplementedException();

        public void Create(Disease item)
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

        public Disease GetItem(int id)
        {
            return context.Diseases.FirstOrDefault(p => p.DiseaseId== id);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Disease item)
        {
            throw new NotImplementedException();
        }
    }
}
