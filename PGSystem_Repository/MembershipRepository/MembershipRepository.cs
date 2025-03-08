using Microsoft.EntityFrameworkCore;
using PGSystem_DataAccessLayer.DBContext;
using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.MembershipRepository
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly AppDBContext _context;

        public MembershipRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<List<Membership>> GetAllMembershipsAsync()
        {
            return await _context.Memberships
                .Where(m => !m.IsDeleted)
                .ToListAsync();
        }

        public async Task<Membership> GetByIdAsync(int membershipId)
        {
            return await _context.Memberships
                .FirstOrDefaultAsync(m => m.MID == membershipId && !m.IsDeleted);
        }
    }
}
