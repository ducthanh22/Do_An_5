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
    public interface IDetail_importbillRepository: IGenericRepository<Detail_importbill>
    {
        //Task<IEnumerable<BaseQuerieResponse<Detail_importbillDto>>> Search(string keywork, int page, int pageSize);
    }
}
