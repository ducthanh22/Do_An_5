using BLL.Interface;
using DAL.Interface;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class StatisticalBus: IStatisticalBus
    {
        public IStatisticalRepository _res;
        public StatisticalBus(IStatisticalRepository res)
        {
            _res = res;
        }
        public async Task<StatisticalDto> Darhboarsh(DateTime? start, DateTime? end)
        {
            return await _res.Darhboarsh(start, end);
        }
    }
}
