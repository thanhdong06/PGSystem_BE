﻿using Microsoft.AspNetCore.Mvc;
using PGSystem.ResponseType;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_Service.Members;
using PGSystem_Service.Memberships;

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
        [HttpPost]
        [Route("Register-Membership")]
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
                int amountInInt = Convert.ToInt32(Math.Round(membershipResult.Amount));

                var clientId = "38bb31de-35a1-4335-8bfa-34ab42934b0a";
                var apiKey = "4d398076-e456-42ab-8ced-149bdce1eb0e";
                var checksumKey = "2067a941fc37077fc1972209419726845f1db43072a0a971ae2169dd0df41e74";

                var payOS = new PayOS(clientId, apiKey, checksumKey);

                var paymentLinkRequest = new PaymentData(
                    orderCode: genOrderCode,
                    amount: amountInInt,
                    description: "Thanh toán đăng ký Membership",
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
