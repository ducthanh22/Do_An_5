using DAL.Interface;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Model;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PaymentRepository : IPaymentRepository
    {
        public readonly  VnPay _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaymentRepository(IOptions<VnPay> config, IHttpContextAccessor httpContextAccessor )
        {
            _config = config.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> CreateURL( PaymentDto paymentDto)
        {
            var tick = DateTime.Now.Ticks.ToString();
            string vnp_Returnurl = _config.vnp_ReturnUrl; //URL nhan ket qua tra ve 
            string vnp_Url = _config.vnp_Url; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = _config.vnp_TmnCode; //Ma website
            string vnp_HashSecret = _config.vnp_HashSecret; //Chuoi bi mat
          
         
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (paymentDto.Money * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_CreateDate", paymentDto.Created?.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString());
            if (!string.IsNullOrEmpty(_config.vnp_Locale))
            {
                vnpay.AddRequestData("vnp_Locale", _config.vnp_Locale);
            }
            else
            {
                vnpay.AddRequestData("vnp_Locale", "vn");
            }
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + paymentDto.OrderId);
            //vnpay.AddRequestData("vnp_OrderType", orderCategory.SelectedItem.Value); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", paymentDto.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày
           
      
            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            return  await   Task.FromResult(paymentUrl);
        }
        public async Task<PaymentRespone> GetPaymentResponse(string query)
        {
            var queryParams = QueryHelpers.ParseQuery(query);

            // Tạo đối tượng PaymentResponse và gán giá trị từ các thông tin phân tích được
            var paymentResponse = new PaymentRespone
            {
                vnp_Amount = queryParams["vnp_Amount"],
                vnp_BankCode = queryParams["vnp_BankCode"],
                vnp_OrderInfo = queryParams["vnp_OrderInfo"],
                vnp_TransactionStatus = queryParams["vnp_TransactionStatus"],
                vnp_TxnRef = queryParams["vnp_TxnRef"]
                // Gán các thuộc tính khác tương tự ở đây nếu cần
            };

            return  await Task.FromResult(paymentResponse);
        }


    }
}
