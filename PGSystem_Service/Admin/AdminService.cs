using AutoMapper;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Repository.Admin;
using PGSystem_Repository.TransactionRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Admin
{
    public class AdminService : IAdminService
    {
       
            private readonly IAdminRepository _adminRepository;
            private readonly ITransactionRepository _transactionRepository;
            private readonly IMapper _mapper;

            public AdminService(IAdminRepository adminRepository, IMapper mapper, ITransactionRepository transactionRepository)
            {
                _adminRepository = adminRepository;
            _transactionRepository = transactionRepository;
                _mapper = mapper;
            }

            public async Task<bool> DeleteMembership(int MID)
            {
                return await _adminRepository.DeleteMembership(MID);
            }

            public async Task<List<UserResponse>> GetAllUsersAsync()
            {
                var users = await _adminRepository.GetAllUsersAsync();

                return users.Select(u => new UserResponse
                {
                    UID = u.UID,
                    FullName = u.FullName,
                    Email = u.Email,
                    Phone = u.Phone,
                    Role = u.Role
                }).ToList();
            }
            public async Task<List<MembershipResponse>> GetResponseMembershipsAsync()
            {
                var memberships = await _adminRepository.GetAllMembershipsAsync();

                return memberships.Select(u => new MembershipResponse
                {
                    Name = u.Name,
                    Description = u.Description,
                    Price = u.Price
                }).ToList();
            }
            public async Task<SystemReportResponse> GetSystemReportAsync()
            {
                return await _adminRepository.GetSystemReportAsync();
            }


            public async Task<MembershipResponse> CreateMembershipAsync(MembershipsRequest request)
            {
                if (await _adminRepository.ExistsByNameAsync(request.Name))
                {
                    throw new System.Exception("Package name already exists.");
                }

                var membership = _mapper.Map<Membership>(request);
                var createdMembership = await _adminRepository.AddAsync(membership);
                return _mapper.Map<MembershipResponse>(createdMembership);
            }

            public async Task<List<MembershipResponse>> GetAllMembershipsAsync()
            {
                var memberships = await _adminRepository.GetAllAsync();
                return memberships.Select(m => _mapper.Map<MembershipResponse>(m)).ToList();
            }
            public async Task<MembershipResponse> UpdateMembershipAsync(int id, MembershipsRequest request)
            {
                var membership = await _adminRepository.GetByIdAsync(id);
                if (membership == null)
                {
                    throw new KeyNotFoundException("Membership not found.");
                }

                // Cập nhật dữ liệu
                membership.Name = request.Name;
                membership.Description = request.Description;
                membership.Price = request.Price;

                await _adminRepository.UpdateAsync(membership);

                return _mapper.Map<MembershipResponse>(membership);
            }


        public async Task<IEnumerable<MemberResponse>> GetAllMembersWithMembershipAsync()
        {
            var members = await _adminRepository.GetAllMembersWithMembershipAsync();
            return _mapper.Map<IEnumerable<MemberResponse>>(members);
        }
        public async Task<List<TransactionEntity>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllTransactionsAsync();
        }
    }
 }


