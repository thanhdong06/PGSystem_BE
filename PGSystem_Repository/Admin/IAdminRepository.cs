using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.Admin
{
    public interface IAdminRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<List<Membership>> GetAllMembershipsAsync();
        Task<SystemReportResponse> GetSystemReportAsync();

    }
}
