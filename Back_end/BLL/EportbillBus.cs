using BUS;
using DAL.Interface;
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EportbillBus : GenericBus<ExportbillDto>, IExportbillBus
    {
        public IExportbillRepository _res;
        public EportbillBus(IExportbillRepository res) : base(res)
        {
            _res = res;
        }
        public async Task<BaseQuerieResponse<ExportbillDto>> Search(int keywork, int page, int pageSize)
        {
            return await _res.Search(keywork, page, pageSize);
        }
    }
}
