using FreshFruit.Models;
using FreshFruit.Services.Momo;
using Microsoft.AspNetCore.Mvc;

namespace FreshFruit.Controllers
{
    public class PaymentController : Controller
    {
        private IMomoService _momoService;       
        public PaymentController(IMomoService momoService)
        {
            _momoService = momoService;

        }
        [HttpPost]
        [Route("Payment/CreateMomoPayment")]
        public async Task<IActionResult> CreateMomoPayment([FromBody] OrderInfo model)
        {
            var response = await _momoService.CreatePaymentMomo(model);
            Console.WriteLine($"Amount gửi sang Momo: {model.Amount}");

            if (string.IsNullOrEmpty(response.PayUrl))
                return BadRequest(new { error = "Không thể tạo URL thanh toán Momo." });

            return Json(new { payUrl = response.PayUrl });
        }


    }
}
