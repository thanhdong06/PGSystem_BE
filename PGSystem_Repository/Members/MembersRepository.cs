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

        public async Task<List<Member>> GetAllMembersAsync()
        {
            return await _context.Members
                .Where(m => !m.IsDeleted) // Bỏ qua thành viên đã bị xóa
                .Include(m => m.User) // Lấy thông tin User
                .Include(m => m.Membership) // Lấy thông tin Membership
                .ToListAsync();
        }

        public async Task<Member> GetMemberByIdAsync(int memberId)
        {
            return await _context.Members
                .Where(m => !m.IsDeleted && m.MemberID == memberId) // Lọc thành viên chưa bị xóa
                .Include(m => m.User) // Lấy thông tin User
                .Include(m => m.Membership) // Lấy thông tin Membership
                .FirstOrDefaultAsync();

        }



            public async Task<Member> GetMemberByUserIdAsync(int userId)
        {
            return await _context.Members
                .Include(m => m.Membership) //neu can lay thong tin cua membership  
                .FirstOrDefaultAsync(m => m.UID == userId && !m.IsDeleted);
        }
        public async Task<User> GetUserByIDAsync(int uid)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UID == uid);
        }
        public async Task<Member> CreateMemberAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }
        public async Task<bool> UpdateMemberAsync(Member member)
        {
            _context.Members.Update(member);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> SoftDeleteMemberAsync(Member member)
        {
            member.IsDeleted = true;
            _context.Members.Update(member);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}


