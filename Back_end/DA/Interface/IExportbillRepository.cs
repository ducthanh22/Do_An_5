
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IExportbillRepository:IGenericRepository<Exportbill>
    {
        Task<BaseQuerieResponse<ExportbillDto>> Search(string keywork, int page, int pageSize);
        Task<CreateExportbillDto> CreateEX(CreateExportbillDto entity);

    }
}
