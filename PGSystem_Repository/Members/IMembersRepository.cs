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
        Task<List<Member>> GetAllMembersAsync();
        Task<Member> GetByIdAsync(int id);
        Task<Member> AddMemberAsync(Member member);
        Task<Member> UpdateMemberAsync(Member member);

        Task<Member> SoftDeleteMemberAsync(Member member);


    }
}
