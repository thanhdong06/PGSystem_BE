using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Admin
{
    public interface IAdminService
    {
        Task<List<UserResponse>> GetAllUsersAsync();
        Task<List<MembershipResponse>> GetResponseMembershipsAsync();
        Task<SystemReportResponse> GetSystemReportAsync();
        Task<MembershipResponse> CreateMembershipAsync(MembershipsRequest request);
        Task<List<MembershipResponse>> GetAllMembershipsAsync();
        Task<bool> DeleteMembership(int MID);
        Task<MembershipResponse> UpdateMembershipAsync(int id, MembershipsRequest request);
        Task<IEnumerable<MemberResponse>> GetAllMembersWithMembershipAsync();

    }
}
