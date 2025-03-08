using Microsoft.AspNetCore.Mvc;
using PGSystem.ResponseType;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_Service.Memberships;

namespace PGSystem.Controllers
{
    [Route("api/Members")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMembershipService _membershipService;

        public MembersController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
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
                string result = await _membershipService.RegisterMembershipAsync(request);
                return Ok(new JsonResponse<string>(result, 200, ""));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(null, 400, ex.Message));
            }
        }
    }
}
