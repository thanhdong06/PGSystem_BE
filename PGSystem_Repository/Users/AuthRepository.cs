using Microsoft.EntityFrameworkCore;
using PGSystem_DataAccessLayer.DBContext;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_DataAccessLayer.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.Users
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDBContext _context;
        private readonly IPasswordService _passwordService;

        public AuthRepository(AppDBContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User?> ValidateUser(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }
            return user;
        }
        public async Task<string> Register(RegisterRequest request)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email || u.Phone == request.Phone);
            if (existingUser != null)
            {
                throw new InvalidDataException("Email or Phone already exists");
            }
            string hashedPassword = _passwordService.HashPassword(request.Password);

            var newUser = new User
            {
                Email = request.Email,
                Password = hashedPassword,
                Phone = request.Phone,
                Role = "User",
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return "Register successfully";
        }
        public async Task<User> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                throw new InvalidDataException("Invalid email !");
            }
            var samePassword = _passwordService.VerifyPassword(request.Password, user.Password);
            if (samePassword) { return user; }
            throw new Exception("Wrong Email Or Password");
        }
    }
}
