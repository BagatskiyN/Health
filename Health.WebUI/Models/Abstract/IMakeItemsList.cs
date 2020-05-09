using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.WebUI.Models.Abstract
{
    interface IMakeItemsList<T> where T:class
    {
        List<T> GetItemsList(int page);
    }
}
