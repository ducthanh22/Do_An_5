using DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Interface
{
    public interface IPaymentBus
    {
        Task<string> CreateURL( PaymentDto paymentDto);
        Task<PaymentRespone> GetPaymentResponse(string query);
    }
}
