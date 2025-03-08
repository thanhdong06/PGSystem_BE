using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.Members
{
    public interface IMembersRepository
    {
        Task<Member> GetMemberByUserIdAsync(int userId);
        Task<Member> GetMemberByIdAsync(int memberId);
        Task<Member> CreateMemberAsync(Member member);
        Task<List<Member>> GetAllMembersAsync();
        Task<bool> UpdateMemberAsync(Member member);
        Task<bool> SoftDeleteMemberAsync(Member member);

    }
}
