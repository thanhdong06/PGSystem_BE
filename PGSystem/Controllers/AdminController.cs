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
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsersAsync();

            if (users == null || users.Count == 0)
            {
                return NotFound(new JsonResponse<List<UserResponse>>(new List<UserResponse>(), StatusCodes.Status404NotFound,"No users found"));
            }
            return Ok(new JsonResponse<List<UserResponse>>(users, StatusCodes.Status200OK,"User list retrieved successfully"));
        }

        [HttpGet("Memberships")]
        public async Task<IActionResult> GetAllMembership()
        {
            var memberships = await _adminService.GetResponseMembershipsAsync();

            if (memberships == null || memberships.Count == 0)
            {
                return NotFound(new JsonResponse<List<MembershipResponse>>(new List<MembershipResponse>(), StatusCodes.Status404NotFound,"No membership found"));
            }
            return Ok(new JsonResponse<List<MembershipResponse>>(memberships, StatusCodes.Status200OK,"Membership list retrieved successfully"));
        }

        [HttpGet("Reports")]
        public async Task<IActionResult> GetSystemReport()
        {
            var report = await _adminService.GetSystemReportAsync();
            return Ok(new JsonResponse<SystemReportResponse>(report, 200, "System report retrieved successfully"));
        }
    }
}
