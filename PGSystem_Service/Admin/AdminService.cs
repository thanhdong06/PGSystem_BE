using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_Repository.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<List<UserResponse>> GetAllUsersAsync()
        {
            var users = await _adminRepository.GetAllUsersAsync();

            return users.Select(u => new UserResponse
            {
                UID = u.UID,
                Email = u.Email,
                Phone = u.Phone,
                Role = u.Role
            }).ToList();
        }
        public async Task<List<MembershipResponse>> GetResponseMembershipsAsync()
        {
            var memberships = await _adminRepository.GetAllMembershipsAsync();

            return memberships.Select(u => new MembershipResponse
            {
                Name = u.Name,
                Description = u.Description,
                Price = u.Price
            }).ToList();
        }
        public async Task<SystemReportResponse> GetSystemReportAsync()
        {
            return await _adminRepository.GetSystemReportAsync();
        }
    }
}
