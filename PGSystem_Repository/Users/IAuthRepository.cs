using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.Users
{
    public interface IAuthRepository
    {
        Task<User> GetUserByIDAsync(int uid);
        Task<User> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();
        Task<User?> ValidateUser(string email, string password);
        Task<string> Register(RegisterRequest request);
        Task<User> LoginAsync(LoginRequest request);
        Task<User> UpdateAsync(User user);
    }
}
