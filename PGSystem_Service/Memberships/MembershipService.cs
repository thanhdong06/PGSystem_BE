using AutoMapper;
using Net.payOS.Types;
using Net.payOS;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Repository.Members;
using PGSystem_Repository.MembershipRepository;
using PGSystem_Repository.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PGSystem_Repository.TransactionRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace PGSystem_Service.Memberships
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMembersRepository _memberRepository;
        private readonly IAuthRepository _authRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public MembershipService(IMembershipRepository membershipRepository,
                                 IMembersRepository memberRepository,
                                 IAuthRepository authRepository,
                                 ITransactionRepository transactionRepository,
                                 IConfiguration configuration,
                                 IMapper mapper)
        {
            _membershipRepository = membershipRepository;
            _memberRepository = memberRepository;
            _authRepository = authRepository;
            _transactionRepository = transactionRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<List<MembershipResponse>> GetAllMembershipsAsync()
        {
            var memberships = await _membershipRepository.GetAllMembershipsAsync();
            return _mapper.Map<List<MembershipResponse>>(memberships);
        }

        public async Task<LoginMemberResponse> RegisterMembershipAsync(RegisterMembershipRequest request, int userId, int orderCode)
        {
            var user = await _authRepository.GetUserByIDAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var membership = await _membershipRepository.GetByIdAsync(request.MembershipId);
            if (membership == null)
                throw new Exception("Membership not found");

            bool isAlreadyMember = await _memberRepository.ExistsByUserIdAsync(userId);
            if (isAlreadyMember)
                throw new Exception("User is already a member!");

            user.Role = "Member";
            await _authRepository.UpdateAsync(user);

            var member = new Member
            {
                UserUID = userId,
                MembershipID = request.MembershipId,
                Status = "Paid",
                OrderCode = orderCode,
                IsDeleted = false
            };

            await _memberRepository.AddMemberAsync(member);

            var transaction = new TransactionEntity
            {
                MemberID = member.MemberID, 
                Amount = membership.Price, 
                TransactionDate = DateTime.Now,
                Status = "Paid",
            };

            await _transactionRepository.AddTransactionAsync(transaction);

            var accessToken = CreateToken(user, 30);    
            var token = new LoginResponse
            {
                Token = accessToken,
                RefreshToken = accessToken,
            };

            return new LoginMemberResponse
            {
                Member = member,
                LoginResponse = token
            };
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

        public async Task<bool> ConfirmMembershipPayment(int orderCode)
        {
            var member = await _memberRepository.GetByOrderCodeAsync(orderCode);
            if (member == null)
            {
                throw new Exception("Membership not found for this order");
            }

            member.Status = "Paid";

            await _memberRepository.UpdateAsync(member);
            var user = await _authRepository.GetUserByIDAsync(member.UserUID);
            if (user == null)
            {
                throw new Exception("User not found for this membership");
            }

            user.Role = "Member";
            await _authRepository.UpdateAsync(user);

            var transaction = await _transactionRepository.GetByMemberIdAsync(member.MemberID);
            if (transaction == null)
            {
                throw new Exception("Transaction not found for this membership");
            }

            transaction.Status = "Completed";
            await _transactionRepository.UpdateAsync(transaction);
            return true;
        }

    }
}
