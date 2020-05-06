using Health.Domain.Abstract;
using Health.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Concrete
{
    public class EFBloodTypeRepository : IRepository<BloodType>
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<BloodType> GetItemList => throw new NotImplementedException();

        public void Create(BloodType item)
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

        public BloodType GetItem(int id)
        {
            return context.BloodTypes.FirstOrDefault(p => p.BloodTypeId == id);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(BloodType item)
        {
            throw new NotImplementedException();
        }
    }
}
