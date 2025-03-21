using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
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
        Task<Member> RegisterMembershipAsync(RegisterMembershipRequest request, int userId, int orderCode);
        Task<bool> ConfirmMembershipPayment(int orderCode);

    }
}
