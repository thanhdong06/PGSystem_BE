using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.Password
{
    public class PasswordService : IPasswordService
    {
        public string GeneratePassword()
        {
            var characters = "qwertyuiopasdfghjklzxcvbnm1234567890!@#$%";
            var random = new Random();
            StringBuilder sb = new StringBuilder();
            while (sb.Length < 7)
            {
                var index = random.Next(characters.Length);
                var character = characters[index];
                sb.Append(character);
            }
            return sb.ToString();
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
