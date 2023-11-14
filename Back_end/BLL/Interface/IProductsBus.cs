using Back_end.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Interface
{
    public interface IProductsBus : IGenericBUS<Products>
    {
        //Task<IEnumerable<Products>> Search(string searchTerm, int page, int pageSize);

    }
}
