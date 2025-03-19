using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Net.payOS;
using Net.payOS.Types;
using PGSystem.ResponseType;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Service.Members;
using PGSystem_Service.Memberships;
using System.Security.Claims;

namespace PGSystem.Controllers
{
    [Route("api/Members")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMembershipService _membershipService;
        private readonly IMembersService _membersService;

        public MembersController(IMembershipService membershipService, IMembersService membersService)
        {
            _membershipService = membershipService;
            _membersService = membersService;
        }

        /// <summary>
        /// Lấy danh sách gói Membership
        /// </summary>
        [HttpGet("Memberships")]
        public async Task<ActionResult<JsonResponse<List<MembershipResponse>>>> GetMembershipPlans()
        {
            try
            {
                var memberships = await _membershipService.GetAllMembershipsAsync();
                return Ok(new JsonResponse<List<MembershipResponse>>(memberships, 200, "Success"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse<List<MembershipResponse>>(null, 500, ex.Message));
            }
        }

        /// <summary>
        /// Đăng ký Membership cho User
        /// </summary>
        [Authorize]
        [HttpPost("Register-Membership")]
        public async Task<ActionResult<JsonResponse<string>>> RegisterMembership([FromBody] RegisterMembershipRequest request)
        {
            try
            {
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return BadRequest(new JsonResponse<string>("User ID not found", 400, ""));
                }
                int userId = int.Parse(userIdClaim.Value);

                // Tạo OrderCode dựa trên timestamp
                var genOrderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));

                var membershipResult = await _membershipService.RegisterMembershipAsync(request, userId, genOrderCode);
                if (membershipResult == null)
                {
                    return BadRequest(new JsonResponse<string>("Failed to register membership", 400, ""));
                }
                int amountInInt = Convert.ToInt32(Math.Round((decimal)membershipResult.Membership.Price));

                var clientId = "7550278f-4d3f-4841-a46d-442d20a0f70c";
                var apiKey = "12f77493-2240-460f-9746-d1a7a9ff2b74";
                var checksumKey = "ae353f468a5c57acde1fff8f985718c32004b27aa969d665d1e5dde26674ae10";

                var payOS = new PayOS(clientId, apiKey, checksumKey);

                var paymentLinkRequest = new PaymentData(
                    orderCode: genOrderCode,
                    amount: amountInInt,
                    description: "Thanh toán Membership",
                    items: [],
                    returnUrl: "http://localhost:3039/payment-successfully",
                    cancelUrl: "http://localhost:3039/payment-cancel"
                );
                var response = await payOS.createPaymentLink(paymentLinkRequest);

                return Ok(new JsonResponse<object>(response, 200, "Create payment request successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something wrong, please contact admin", 400, ex.Message));
            }
        }

        [HttpGet("verify-payment")]
        public async Task<ActionResult<JsonResponse<string>>> VerifyPayment([FromQuery] int orderCode)
        {
            try
            {
                var clientId = "7550278f-4d3f-4841-a46d-442d20a0f70c";
                var apiKey = "12f77493-2240-460f-9746-d1a7a9ff2b74";
                var checksumKey = "ae353f468a5c57acde1fff8f985718c32004b27aa969d665d1e5dde26674ae10";

                var payOS = new PayOS(clientId, apiKey, checksumKey);
                var paymentStatus = await payOS.getPaymentLinkInformation(orderCode);

                if (paymentStatus != null && paymentStatus.status == "PAID")
                {
                    await _membershipService.ConfirmMembershipPayment(orderCode);
                    return Ok(new JsonResponse<string>("Payment successful", 200, ""));
                }
                return BadRequest(new JsonResponse<string>("Payment not completed yet", 400, ""));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Error verifying payment", 400, ex.Message));
            }
        }

        [HttpPut("update-membership")]
        public async Task<IActionResult> UpdateMembership([FromBody] MemberShipUpdateRequest request)
        {
            try
            {
                var response = await _membersService.UpdateMembershipAsync(request);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }

        [HttpDelete("delete-membership/{userUID}")]
        public async Task<IActionResult> SetMembershipToThree(int userUID)
        {
            try
            {
                var response = await _membersService.DeleteMembershipAsync(userUID);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }
    }
}
