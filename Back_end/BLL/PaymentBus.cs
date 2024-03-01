using BUS.Interface;
using DAL.Interface;
using DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class PaymentBus : IPaymentBus
    {
        public readonly IPaymentRepository _res;

        public PaymentBus(IPaymentRepository res) {
            _res = res;
        }
        public async Task<string> CreateURL( PaymentDto paymentDto)
        {
            return await _res.CreateURL( paymentDto);
        }

        public async Task<PaymentRespone> GetPaymentResponse(string query)
        {
            return await _res.GetPaymentResponse(query);  
        }
    }
}
