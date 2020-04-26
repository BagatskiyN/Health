using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Abstract
{
   public interface IRepository<T>:IDisposable
               where T : class
    {
        IEnumerable<T> GetItemList { get; }
        T GetItem(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();

    }
}
