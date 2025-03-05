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
            return await _context.Members.ToListAsync();
        }

        public async Task<Member> GetByIdAsync(int id)
        {
            // Tìm thành viên theo ID và tải kèm dữ liệu liên quan
            var member = await _context.Members
                .Where(m => m.MemberID == id)  // Lọc theo ID
                .Include(m => m.Membership)    // Tải thông tin Membership
                .Include(m => m.Blogs)         // Tải danh sách Blogs
                .Include(m => m.Comments)      // Tải danh sách Comments
                .Include(m => m.Reminders)     // Tải danh sách Reminders
                .Include(m => m.PregnancyRecord) // Tải thông tin Pregnancy Record (nếu có)
                .FirstOrDefaultAsync(); // Lấy phần tử đầu tiên hoặc trả về null nếu không tìm thấy     

            return member;
        }

        public async Task<Member> AddMemberAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }


        public async Task<Member?> GetMemberAsync(int id)
        {
            return await _context.Members
                .Include(m => m.Membership)
                .FirstOrDefaultAsync(m => m.MemberID == id);
        }

        public async Task<Member> UpdateMemberAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<Member> SoftDeleteMemberAsync(Member member)
        {
            member.IsDeleted = true;
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
            return member;

        }
    }
}

