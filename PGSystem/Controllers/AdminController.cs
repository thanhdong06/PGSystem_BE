using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGSystem.ResponseType;
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
    }
}
