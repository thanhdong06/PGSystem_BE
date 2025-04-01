using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Types;
using PGSystem.ResponseType;
using PGSystem_Service.Members;
using PGSystem_Service.Memberships;
using System.Security.Claims;

namespace PGSystem.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : Controller
    {
        private readonly IMembershipService _membershipService;
        private IMembersService _membersService;

        public PaymentController(IMembershipService membershipService, IMembersService membersService)
        {
            _membershipService = membershipService;
            _membersService = membersService;
        }
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
                returnUrl: "http://localhost:5173/payment-successfully",
                cancelUrl: "http://localhost:5173/payment-cancel"
            );
            var response = await payOS.createPaymentLink(paymentLinkRequest);

            return Ok(new JsonResponse<object>(response, 400, "create payment request successfully"));
        }

        //[HttpPost]
        //public async Task<IActionResult> WebHookPayOs([FromBody] WebhookType webhookType)
        //{
        //    try
        //    {
        //        var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        //        if (userIdClaim == null)
        //        {
        //            return BadRequest(new JsonResponse<string>("User ID not found", 400, ""));
        //        }
        //        int userId = int.Parse(userIdClaim.Value);

        //        // Tạo OrderCode dựa trên timestamp
        //        var genOrderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));

        //        var membershipResult = await _membershipService.RegisterMembershipAsync(request, userId, genOrderCode);
        //        if (membershipResult == null)
        //        {
        //            return BadRequest(new JsonResponse<string>("Failed to register membership", 400, ""));
        //        }
        //        int amountInInt = Convert.ToInt32(Math.Round((decimal)membershipResult.Membership.Price));

        //        var clientId = "7550278f-4d3f-4841-a46d-442d20a0f70c";
        //        var apiKey = "12f77493-2240-460f-9746-d1a7a9ff2b74";
        //        var checksumKey = "ae353f468a5c57acde1fff8f985718c32004b27aa969d665d1e5dde26674ae10";

        //        var payOS = new PayOS(clientId, apiKey, checksumKey);

        //        var paymentLinkRequest = new PaymentData(
        //            orderCode: genOrderCode,
        //            amount: amountInInt,
        //            description: "Thanh toán Membership",
        //            items: [],
        //            returnUrl: "http://localhost:5173/payment-successfully",
        //            cancelUrl: "http://localhost:5173/payment-cancel"
        //        );
        //        var response = await payOS.createPaymentLink(paymentLinkRequest);

        //        return Ok(new JsonResponse<object>(new
        //        {
        //            userID = userId,
        //            paymentResponse = response
        //        }, 200, "Create payment request successfully"));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new JsonResponse<string>("Something wrong, please contact admin", 400, ex.Message));
        //    }
        //} 
    }
}
