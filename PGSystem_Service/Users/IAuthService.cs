using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
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
        Task<string> RegisterUserAsync(RegisterRequest registerRequest);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<string> RefreshTokenAsync(string refreshToken);
    }
}
