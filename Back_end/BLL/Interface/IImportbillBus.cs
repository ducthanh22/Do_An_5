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
    public  interface IImportbillBus : IGenericBUS<Importbill>
    {
        Task<BaseQuerieResponse<ImportbillDto>> Search(int keywork, int page, int pageSize);
        Task<CreateImportbillDto> CreateIm(CreateImportbillDto entity);

    }
}
