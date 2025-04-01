using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGSystem.ResponseType;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_Service.Admin;

namespace PGSystem.Controllers
{
    [Route("api/Admin")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet("Users")]
        public async Task<ActionResult<JsonResponse<List<UserResponse>>>> GetAllUsers()
        {
            try
            {
                var users = await _adminService.GetAllUsersAsync();

                if (users == null || users.Count == 0)
                {
                    return NotFound(new JsonResponse<List<UserResponse>>(new List<UserResponse>(), 404, "No users found"));
                }

                return Ok(new JsonResponse<List<UserResponse>>(users, 200, "User list successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse<List<UserResponse>>(new List<UserResponse>(), 500, "An error occurred while retrieving users"));
            }
        }
        [HttpGet("Memberships")]
        public async Task<ActionResult<JsonResponse<List<MembershipResponse>>>> GetAllMembership()
        {
            try
            {
                var memberships = await _adminService.GetResponseMembershipsAsync();

                if (memberships == null || memberships.Count == 0)
                {
                    return NotFound(new JsonResponse<List<MembershipResponse>>(new List<MembershipResponse>(), 404, "No membership found"));
                }

                return Ok(new JsonResponse<List<MembershipResponse>>(memberships, 200, "Membership list successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse<List<MembershipResponse>>(new List<MembershipResponse>(), 500, "An error occurred while retrieving memberships"));
            }
        }
        [HttpGet("Reports")]
        public async Task<ActionResult<JsonResponse<SystemReportResponse>>> GetSystemReport()
        {
            try
            {
                var report = await _adminService.GetSystemReportAsync();

                if (report == null)
                {
                    return NotFound(new JsonResponse<SystemReportResponse>(null, 404, "System report not found"));
                }

                return Ok(new JsonResponse<SystemReportResponse>(report, 200, "System report retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse<SystemReportResponse>(null, 500, "An error occurred while retrieving the system report"));
            }
        }

        [HttpGet("Transactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _adminService.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        [HttpDelete("DeleteMembership")]
        public async Task<ActionResult<JsonResponse<string>>> DeleteMembership(int MID)
        {
            try
            {
                var isDeleted = await _adminService.DeleteMembership(MID);

                if (!isDeleted)
                {
                    return NotFound(new JsonResponse<string>(null, 404, "Membership not found"));
                }

                return Ok(new JsonResponse<string>(null, 200, "Membership deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse<string>(null, 500, "An error occurred while deleting the Membership"));
            }
        }
        [HttpPut("UpdateMembership/{id}")]
        public async Task<IActionResult> UpdateMembership(int id, [FromBody] MembershipsRequest request)
        {
            var updatedMembership = await _adminService.UpdateMembershipAsync(id, request);
            return Ok(updatedMembership);
        }
        [HttpPost("CreateMembership")]
        public async Task<ActionResult<JsonResponse<string>>> CreateMembership([FromBody] MembershipsRequest request)
        {
            try
            {
                await _adminService.CreateMembershipAsync(request);
                return Ok(new JsonResponse<string>(null, 200, "Memberships created successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact the admin", 400, ex.Message));
            }
        }
        [HttpGet("with-membership")]
        public async Task<IActionResult> GetMembersWithMembership()
        {
            var members = await _adminService.GetAllMembersWithMembershipAsync();
            return Ok(members);
        }

    }
}