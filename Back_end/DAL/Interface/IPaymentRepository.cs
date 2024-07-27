using DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IPaymentRepository
    {
        Task<string>CreateURL(PaymentDto paymentDto);
        Task<PaymentRespone> GetPaymentResponse(string query);
    }
}
