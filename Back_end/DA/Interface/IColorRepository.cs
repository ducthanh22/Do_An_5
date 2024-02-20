using System;
using System.Collections.Generic;
using Model;
using DTO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IColorRepository : IGenericRepository<Color>
    {
        Task<UpLoadFile> UploadFile(UpLoadFile color);

    }
}
