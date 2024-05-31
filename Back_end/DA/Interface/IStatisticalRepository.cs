using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IStatisticalRepository
    {
        Task<StatisticalDto> Darhboarsh(DateTime? start, DateTime? end);
    }
}
