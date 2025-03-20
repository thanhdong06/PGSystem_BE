using Microsoft.EntityFrameworkCore;
using PGSystem_DataAccessLayer.DBContext;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Repository.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.Members
{
    public class MembersRepository : IMembersRepository
    {
        private readonly AppDBContext _context;
        public MembersRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<bool> ExistsByUserIdAsync(int userId)
        {
            return await _context.Members.AnyAsync(m => m.UserUID == userId && !m.IsDeleted);
        }

        public async Task AddMemberAsync(Member member)
        {
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        //public async Task<Member> GetMemberByUserIdAsync(int userId)
        //{
        //    return await _context.Members
        //        .Include(m => m.Membership) //neu can lay thong tin cua membership  
        //        .FirstOrDefaultAsync(m => m.UID == userId && !m.IsDeleted);
        //}
        //public async Task<User> GetUserByIDAsync(int uid)
        //{
        //    return await _context.Users.FirstOrDefaultAsync(u => u.UID == uid);
        //}
        //public async Task<Member> CreateMemberAsync(Member member)
        //{
        //    _context.Members.Add(member);
        //    await _context.SaveChangesAsync();
        //    return member;
        //}
        //public async Task<bool> UpdateMemberAsync(Member member)
        //{
        //    _context.Members.Update(member);
        //    return await _context.SaveChangesAsync() > 0;
        //}
        //public async Task<bool> SoftDeleteMemberAsync(Member member)
        //{
        //    member.IsDeleted = true;
        //    _context.Members.Update(member);
        //    return await _context.SaveChangesAsync() > 0;
        //}
        public async Task<Member> GetMemberShipIdByUserUIDAsync(int userUID)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.UserUID == userUID);
        }
        public async Task<Member> GetByOrderCodeAsync(int orderCode)
        {
            return await _context.Members
                .Include(m => m.Membership)
                .FirstOrDefaultAsync(m => m.OrderCode == orderCode);
        }

        public async Task UpdateAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }


        public async Task<Member> GetMemberByIdAsync(int memberId)
        {
            return await _context.Members
                .Include(m => m.User)
                .Include(m => m.Membership)
                .FirstOrDefaultAsync(m => m.MemberID == memberId);



        }


        public async Task DeleteMemberAsync(Member member)
        {
            _context.Members.Remove(member);

            // Cập nhật role của User thành "User"
            var user = member.User;
            if (user != null)
            {
                user.Role = "User";

            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
