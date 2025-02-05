using PGSystem_DataAccessLayer.DTO.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Users
{
    public interface IAuthService
    {
        Task<LoginGoogleRequest> CreateUser(string email);
    }
}
