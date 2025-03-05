using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Types;
using PGSystem.ResponseType;

namespace PGSystem.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Create(int orderCode, int amount)
        {
            var clientId = "7550278f-4d3f-4841-a46d-442d20a0f70c";
            var apiKey = "12f77493-2240-460f-9746-d1a7a9ff2b74";
            var checksumKey = "ae353f468a5c57acde1fff8f985718c32004b27aa969d665d1e5dde26674ae10";

            var payOS = new PayOS(clientId, apiKey, checksumKey);

            var paymentLinkRequest = new PaymentData(
                orderCode: orderCode,
                amount: amount,
                description: "Thanh toan don hang",
                items: [],
                returnUrl: "http://localhost:3039/payment-successfully",
                cancelUrl: "http://localhost:3039/payment-cancel"
            );
            var response = await payOS.createPaymentLink(paymentLinkRequest);

            return Ok(new JsonResponse<object>(response, 400, "create payment request successfully"));
        }
    }
}
