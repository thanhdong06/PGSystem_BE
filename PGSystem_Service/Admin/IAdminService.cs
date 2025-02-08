using PGSystem_DataAccessLayer.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Admin
{
    public interface IAdminService
    {
        Task<List<ResponseUser>> GetAllUsersAsync();
        Task<List<ResponseMembership>> GetResponseMembershipsAsync();
    }
}
