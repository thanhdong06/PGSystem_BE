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

namespace PGSystem_Service.Memberships
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMembersRepository _memberRepository;
        private readonly IAuthRepository _authRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public MembershipService(IMembershipRepository membershipRepository,
                                 IMembersRepository memberRepository,
                                 IAuthRepository authRepository,
                                 ITransactionRepository transactionRepository,
                                 IMapper mapper)
        {
            _membershipRepository = membershipRepository;
            _memberRepository = memberRepository;
            _authRepository = authRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<List<MembershipResponse>> GetAllMembershipsAsync()
        {
            var memberships = await _membershipRepository.GetAllMembershipsAsync();
            return _mapper.Map<List<MembershipResponse>>(memberships);
        }

        public async Task<Member> RegisterMembershipAsync(RegisterMembershipRequest request, int userId, int orderCode)
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
            return member;
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
