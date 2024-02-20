using System;
using System.Collections.Generic;
using Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BUS.Interface
{
    public interface IColorBus :IGenericBUS<Color>
    {
        Task<UpLoadFile> UploadFile(UpLoadFile color);

    }
}
