using AutoMapper;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Repository.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Users
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public AuthService(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }
        public async Task<LoginGoogleRequest> CreateUser(string email)
        {
            var user = await _authRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    Role = "User"
                };

                await _authRepository.AddUserAsync(user);
                await _authRepository.SaveChangesAsync();
            }

            return _mapper.Map<LoginGoogleRequest>(user);
        }
    }
}
