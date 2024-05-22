
using BLL.Interface;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IExportbillBus :IGenericBUS<Exportbill>
    {
        Task<BaseQuerieResponse<ExportbillDto>> Search(Paging paging);
        Task<CreateExportbillDto> CreateEX(CreateExportbillDto entity);
        Task<Exportbill> DELETE(Guid id);

    }
}
