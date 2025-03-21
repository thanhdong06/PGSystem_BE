using AutoMapper;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_Repository.Admin;
using PGSystem_Repository.Members;
using PGSystem_Service.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PGSystem_Repository.Admin;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Repository.Users;
using Microsoft.AspNetCore.Identity;

namespace PGSystem_Service.Members
{
    public class MembersService : IMembersService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IMembersRepository _memberRepository;
        private readonly IMapper _mapper;
        
        public MembersService(IMembersRepository membersRepository, IMapper mapper, IAdminRepository adminRepository )
        {
            _memberRepository = membersRepository;
            _mapper = mapper;
            _adminRepository = adminRepository;         
        }
        //public async Task<List<MemberResponse>> GetAllMembersAsync()
        //{
        //    var members = await _memberRepository.GetAllMembersAsync();
        //    return _mapper.Map<List<MemberResponse>>(members);
        //}

        //public async Task<MemberResponse> GetMemberByIdAsync(int memberId)
        //{
        //    var member = await _memberRepository.GetMemberByIdAsync(memberId);
        //    if (member == null)
        //    {
        //        throw new KeyNotFoundException("Member not found.");
        //    }

        //    return _mapper.Map<MemberResponse>(member);
        //}

        //public async Task<MemberResponse> RegisterMemberAsync(MemberRequest request)
        //{
        //    // Kiểm tra xem User có tồn tại không
        //    var user = await _memberRepository.GetMemberByUserIdAsync(request.UID);
        //    if (user == null)
        //    {
        //        throw new KeyNotFoundException("User does not exist.");
        //    }

        //    // Kiểm tra nếu User đã có Membership chưa
        //    var existingMember = await _memberRepository.GetMemberByUserIdAsync(request.UID);
        //    if (existingMember != null)
        //    {
        //        throw new InvalidOperationException("User already has a membership.");
        //    }

        //    // Kiểm tra xem Membership có tồn tại không
        //    var membership = await _adminRepository.GetByIdAsync(request.MembershipID);
        //    if (membership == null || membership.IsDeleted)
        //    {
        //        throw new KeyNotFoundException("Invalid membership plan.");
        //    }

        //    // Tạo mới Membership cho User
        //    var newMember = new Member
        //    {
        //        UID = request.UID,
        //        MembershipID = request.MembershipID,
        //        Membership = membership,
        //        IsDeleted = false
        //    };

        //    var createdMember = await _memberRepository.CreateMemberAsync(newMember);
        //    return _mapper.Map<MemberResponse>(createdMember);
        //}
        //public async Task<bool> UpdateMemberAsync(int memberId, MemberRequest request)
        //{
        //    // 1. Tìm thành viên
        //    var member = await _memberRepository.GetMemberByUserIdAsync(memberId);
        //    if (member == null)
        //    {
        //        throw new KeyNotFoundException("Member not found.");
        //    }

        //    // 2. Kiểm tra MembershipID hợp lệ
        //    var membership = await _adminRepository.GetByIdAsync(request.MembershipID);
        //    if (membership == null || membership.IsDeleted)
        //    {
        //        throw new KeyNotFoundException("Invalid membership plan.");
        //    }

        //    // 3. Cập nhật thông tin
        //    //member.User.FullName = request.FullName;
        //    member.MembershipID = request.MembershipID;
        //    member.Membership = membership;

        //    return await _memberRepository.UpdateMemberAsync(member);
        //}
        //public async Task<bool> DeleteMemberAsync(int memberId)
        //{
        //    // 1. Tìm thành viên
        //    var member = await _memberRepository.GetMemberByUserIdAsync(memberId);
        //    if (member == null || member.IsDeleted)
        //    {
        //        throw new KeyNotFoundException("Member not found or already deleted.");
        //    }

        //    // 2. Hủy tư cách thành viên (xóa mềm)
        //    return await _memberRepository.SoftDeleteMemberAsync(member);
        //}


        public async Task<MemberResponse> UpdateMembershipAsync(MemberShipUpdateRequest request)
        {
            var member = await _memberRepository.GetMemberShipIdByUserUIDAsync(request.UserUID);
            if (member == null)
            {
                throw new KeyNotFoundException("Member not found.");
            }

            member.MembershipID = request.NewMembershipID;
            await _memberRepository.UpdateAsync(member);
            await _memberRepository.SaveChangesAsync();

            return _mapper.Map<MemberResponse>(member);
        }

       /* public async Task<MemberResponse> DeleteMembershipAsync(int userUID)
        {
            var member = await _memberRepository.GetMemberShipIdByUserUIDAsync(userUID);
            if (member == null)
            {
                throw new KeyNotFoundException("Member not found.");
            }

            member.MembershipID = 3;
            await _memberRepository.UpdateAsync(member);
            await _memberRepository.SaveChangesAsync();

            return _mapper.Map<MemberResponse>(member);
        }*/

        public async Task<DeleteMemberResponse> DeleteMemberAsync(DeleteMemberRequest request)
        {
            var member = await _memberRepository.GetMemberByID(request.MemberID);
            if (member == null)
            {
                throw new KeyNotFoundException("Member not found");
            }

            await _memberRepository.DeleteMemberAsync(member);
            await _memberRepository.SaveChangesAsync();

            return _mapper.Map<DeleteMemberResponse>(member);
        }
    }
}
    

