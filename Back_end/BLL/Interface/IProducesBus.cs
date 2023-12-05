
using BUS.Interface;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IProducesBus : IGenericBUS<Produces>
    {
        Task<BaseQuerieResponse<ProducesDto>> Search(string keywork, int page, int pageSize);
    }
}
