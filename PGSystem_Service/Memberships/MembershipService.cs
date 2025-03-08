using AutoMapper;
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

namespace PGSystem_Service.Memberships
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMembersRepository _memberRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public MembershipService(IMembershipRepository membershipRepository,
                                 IMembersRepository memberRepository,
                                 IAuthRepository authRepository,
                                 IMapper mapper)
        {
            _membershipRepository = membershipRepository;
            _memberRepository = memberRepository;
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<List<MembershipResponse>> GetAllMembershipsAsync()
        {
            var memberships = await _membershipRepository.GetAllMembershipsAsync();
            return _mapper.Map<List<MembershipResponse>>(memberships);
        }

        public async Task<string> RegisterMembershipAsync(RegisterMembershipRequest request)
        {
            var userExists = await _authRepository.GetUserByIDAsync(request.UserId);
            if (userExists == null)
                throw new Exception("User not found");

            var membership = await _membershipRepository.GetByIdAsync(request.MembershipId);
            if (membership == null)
                throw new Exception("Membership not found");

            bool isAlreadyMember = await _memberRepository.ExistsByUserIdAsync(request.UserId);
            if (isAlreadyMember)
                throw new Exception("User is already a member!");

            var member = new Member
            {
                UserUID = request.UserId,
                MembershipID = request.MembershipId,
                IsDeleted = false
            };

            await _memberRepository.AddMemberAsync(member);
            return "Membership registered successfully!";
        }
    }
}
