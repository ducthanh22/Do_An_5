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
    public class ImportbillBus : GenericBus<Importbill>, IImportbillBus
    {
        public IImportbillRepository _res;
        public ImportbillBus(IImportbillRepository res) : base(res)
        {
            _res = res;
        }
        public async Task<BaseQuerieResponse<ImportbillDto>> Search(Paging paging)
        {
            return await _res.Search(paging);
        }
        public async Task<CreateImportbillDto> CreateIm(CreateImportbillDto entity)
        {
            return await _res.CreateIm(entity);
        }

    }
}
