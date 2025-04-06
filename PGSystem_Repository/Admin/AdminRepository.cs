using Microsoft.EntityFrameworkCore;
using PGSystem_DataAccessLayer.DBContext;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    namespace PGSystem_Repository.Admin
    {
        public class AdminRepository : IAdminRepository
        {
            private readonly AppDBContext _context;
            public AdminRepository(AppDBContext context)
            {
                _context = context;
            }
            public async Task<List<User>> GetAllUsersAsync()
            {
                return await _context.Users.ToListAsync();
            }
            public async Task<List<Membership>> GetAllMembershipsAsync()
            {
                return await _context.Memberships.Where(m => !m.IsDeleted).ToListAsync();
            }
            public async Task<SystemReportResponse> GetSystemReportAsync()
            {
                var totalUsers = await _context.Users.CountAsync();
                var totalAdmins = await _context.Users.CountAsync(u => u.Role == "Admin");
                var totalMembers = await _context.Users.CountAsync(u => u.Role == "Member");
            var totalTransaction = await _context.Transactions.CountAsync(u => u.Status == "Paid");
                return new SystemReportResponse
                {
                    TotalUsers = totalUsers,
                    TotalAdmins = totalAdmins,
                    TotalMembers = totalMembers,
                    TotalTransactions = totalTransaction,
                    ReportDate = DateTime.UtcNow
                };

            }

            public async Task<Membership> AddAsync(Membership membership)
            {
                _context.Memberships.Add(membership);
                await _context.SaveChangesAsync();
                return membership;
            }

            public async Task<bool> ExistsByNameAsync(string name)
            {
                return await _context.Memberships.AnyAsync(m => m.Name == name);
            }

            public async Task<List<Membership>> GetAllAsync()
            {
                return await _context.Memberships.Where(m => !m.IsDeleted).ToListAsync();
            }

            public async Task<bool> DeleteMembership(int MID)
            {
                var membership = await _context.Memberships.FirstOrDefaultAsync(gr => gr.MID == MID && !gr.IsDeleted);

                if (membership == null)
                {
                    return false;
                }
                membership.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<Membership> GetByIdAsync(int id)
            {
                return await _context.Memberships.FindAsync(id);
            }

            public async Task UpdateAsync(Membership membership)
            {
                _context.Memberships.Update(membership);
                await _context.SaveChangesAsync();
            }

        public async Task<IEnumerable<Member>> GetAllMembersWithMembershipAsync()
        {
            return await _context.Members
                .Include(m => m.User)
                .Include(m => m.Membership)
                .Where(m => m.MembershipID != 0) // Lọc những thành viên có Membership
                .ToListAsync();
        }

        public async Task<List<Member>> GetAllWithUserAndMembershipAsync()
        {
            return await _context.Members
                .Where(m => !m.IsDeleted)
                .Include(m => m.User)
                .Include(m => m.Membership)
                .ToListAsync();
        }
    }
    }
