using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Memberships
{
    public interface IMembershipService
    {
        Task<List<MembershipResponse>> GetAllMembershipsAsync();
        Task<string> RegisterMembershipAsync(RegisterMembershipRequest request);
    }
}
