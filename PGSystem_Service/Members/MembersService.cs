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

namespace PGSystem_Service.Members
{
    public class MembersService : IMembersService


    {

        private readonly IAdminRepository _adminRepository;
         
        private readonly IMembersRepository _memberRepository;
        private readonly IMapper _mapper;
        public MembersService(IMembersRepository membersRepository, IMapper mapper, IAdminRepository adminRepository)
        {
            _memberRepository = membersRepository;
            _mapper = mapper;
            _adminRepository = adminRepository;
            
        }

        public async Task<List<MemberResponse>> GetAllMembersAsync()
        {
            var members = await _memberRepository.GetAllMembersAsync();

            return members.Select(m => new MemberResponse
            {
               MemberID = m.MemberID,
               MemberName =m.MemberName,
            }) .ToList();
        }

        public async Task<MemberResponse> GetMemberByIdAsync(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null)
            {
                throw new KeyNotFoundException("Member not found.");
            }

            return _mapper.Map<MemberResponse>(member);
        }

        public async Task<MemberResponse> RegisterMemberAsync(MemberRequest request)
        {
            // Kiểm tra MembershipID hợp lệ
            var membership = await _adminRepository.GetByIdAsync(request.MembershipID);
            if (membership == null)
            {
                throw new KeyNotFoundException("Membership not found.");
            }

            // Tạo đối tượng Member từ request
            var newMember = _mapper.Map<Member>(request);
            newMember.Membership = membership;

            // Thêm vào database
            var createdMember = await _memberRepository.AddMemberAsync(newMember);

            // Chuyển đổi thành DTO Response và trả về
            return _mapper.Map<MemberResponse>(createdMember);
        }



        public async Task<MemberResponse> UpdateMemberAsync(int id, MemberRequest request)
        {
            // Tìm thành viên theo ID
            var existingMember = await _memberRepository.GetByIdAsync(id);
            if (existingMember == null)
            {
                throw new KeyNotFoundException("Member not found.");
            }

            // Kiểm tra MembershipID hợp lệ
            var membership = await _adminRepository.GetByIdAsync(request.MembershipID);
            if (membership == null)
            {
                throw new KeyNotFoundException("Membership not found.");
            }

            // Cập nhật thông tin
            _mapper.Map(request, existingMember);
            existingMember.Membership = membership;

            // Lưu vào DB
            var updatedMember = await _memberRepository.UpdateMemberAsync(existingMember);

            // Trả về DTO Response
            return _mapper.Map<MemberResponse>(updatedMember);
        }



        public async Task<bool> SoftDeleteMemberAsync(int id)
        {
            // Tìm thành viên theo ID
            var existingMember = await _memberRepository.GetByIdAsync(id);
            if (existingMember == null)
            {
                throw new KeyNotFoundException("Member not found.");
            }

            // Thực hiện xóa mềm
            await _memberRepository.SoftDeleteMemberAsync(existingMember);
            return true;
        }

    }

}

