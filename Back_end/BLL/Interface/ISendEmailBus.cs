using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Interface
{
    public interface ISendEmailBus
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
