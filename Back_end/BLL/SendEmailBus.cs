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
    public class SendEmailBus : ISendEmailBus
    {
        public ISendEmailRepository _res { get; set; }

        public SendEmailBus(ISendEmailRepository res) 
        {
            _res = res;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
              await _res.SendEmailAsync(email, subject, htmlMessage);
        }
    }
}
