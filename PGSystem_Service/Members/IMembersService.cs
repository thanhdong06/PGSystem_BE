using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Members
{
    public interface IMembersService
    {
        Task<List<MemberResponse>> GetAllMembersAsync();
        Task<MemberResponse> GetMemberByIdAsync(int id);
        Task<MemberResponse> RegisterMemberAsync(MemberRequest request);
        Task<MemberResponse> UpdateMemberAsync(int id, MemberRequest request);
        Task<bool> SoftDeleteMemberAsync(int id);
    }

}

