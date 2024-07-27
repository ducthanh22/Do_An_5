using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Threading.Tasks;
using DTO;

namespace DAL.Interface
{
    public interface ISizeRepository: IGenericRepository<Size>
    {
        Task<List<SizeDto>> Getbyidproduct(Guid id);
    }
}
