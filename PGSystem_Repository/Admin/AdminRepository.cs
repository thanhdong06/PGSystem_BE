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
            return await _context.Memberships.ToListAsync();
        }
    }
}
