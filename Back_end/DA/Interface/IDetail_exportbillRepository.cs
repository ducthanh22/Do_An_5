using Back_end.Model;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IDetail_exportbillRepository : IGenericRepository<Detail_exportbill>
    {
        //Task<BaseQuerieResponse<Detail_exportbillDto>>Search(string keywork, int page, int pageSize);
    }
}
