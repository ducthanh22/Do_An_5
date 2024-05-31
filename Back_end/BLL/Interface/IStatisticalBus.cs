using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IStatisticalBus
    {
        Task<StatisticalDto> Darhboarsh(DateTime? start, DateTime? end);
    }
}
