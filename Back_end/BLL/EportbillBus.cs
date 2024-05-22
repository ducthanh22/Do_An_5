using BLL.Interface;
using DAL.Interface;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EportbillBus : GenericBus<Exportbill>, IExportbillBus
    {
        public IExportbillRepository _res;
        public EportbillBus(IExportbillRepository res) : base(res)
        {
            _res = res;
        }
        public async Task<BaseQuerieResponse<ExportbillDto>> Search(Paging paging)
        {
            return await _res.Search(paging);
        }
       
        public async Task<CreateExportbillDto> CreateEX(CreateExportbillDto entity)
        {
            return await _res.CreateEX(entity);
        }
        public async Task<Exportbill> DELETE(Guid id)
        {
            return await _res.DELETE(id);

        }
    }
}
