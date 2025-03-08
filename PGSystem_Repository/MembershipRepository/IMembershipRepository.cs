using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.MembershipRepository
{
    public interface IMembershipRepository
    {
        Task<List<Membership>> GetAllMembershipsAsync();
        Task<Membership> GetByIdAsync(int membershipId);
    }
}
