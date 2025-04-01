using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PGSystem_DataAccessLayer.DBContext;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_DataAccessLayer.Password;
using PGSystem_Repository.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Users
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IMapper mapper, IPasswordService passwordService, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _passwordService = passwordService;
            _configuration = configuration;
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
        public async Task<string> RegisterUserAsync(RegisterRequest registerRequest)
        {
            var result = await _authRepository.Register(registerRequest);
            return result;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _authRepository.LoginAsync(request);

            if (user == null)
            {
                throw new InvalidDataException("Invalid phone number or password");
            }
            var accessToken = CreateToken(user, 30);  
            var refreshToken = CreateToken(user, 7 * 24 * 60);  

            var responseUser = new UserResponse
            {
                UID = user.UID,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role,
                FullName = user.FullName,
                MemberId = user.Member?.MemberID,
            };  

            return new LoginResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                User = responseUser
            };
        }
        public async Task<string> RefreshTokenAsync(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:secret"]);

            try
            {
                var principal = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = false
                }, out SecurityToken validatedToken);
                var tokenTypeClaim = principal.Claims.FirstOrDefault(c => c.Type == "tokenType")?.Value;
                if (tokenTypeClaim != "access") 
                {
                    throw new SecurityTokenException("Invalid refresh token");
                }
                var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var phone = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var role = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                var newAccessToken = CreateToken(new User
                {
                    UID = int.Parse(userId),
                    Phone = phone,
                    Role = role
                }, 30); // Access Token 30 phút

                return newAccessToken;
            }
            catch
            {
                throw new SecurityTokenException("Invalid refresh token");
            }
        }
        private string CreateToken(User user, int expireMinutes)
        {
            if (user == null)
            {
                throw new ArgumentException("Invalid login request", nameof(user));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:secret"]);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UID.ToString()),
                new Claim(ClaimTypes.Name, user.Phone.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("tokenType", "access")

            };
            if (user.Member != null)
            {
                claims.Add(new Claim("MemberId", user.Member.MemberID.ToString()));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:issuer"],
                Audience = _configuration["Jwt:audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<UserResponse?> UpdateUserAsync(int uid, UserUpdateRequest userUpdateRequest)
        {
            var user = await _authRepository.GetUserByIDAsync(uid);
            if (user == null)
            {
                return null;
            }

            user.Phone = userUpdateRequest.Phone;
            user.FullName = userUpdateRequest.FullName;
            user.UpdateAt = DateTime.UtcNow;

            var updatedUser = await _authRepository.UpdateAsync(user);
            return _mapper.Map<UserResponse>(updatedUser);
        }

        public async Task<UserResponse> GetUserByIdAsync(int uid)
        {
            var user = await _authRepository.GetUserByIDAsync(uid);
            return _mapper.Map<UserResponse>(user);
        }
    }
}
