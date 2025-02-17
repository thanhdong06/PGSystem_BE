using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;

namespace PGSystem_DataAccessLayer.MappingAndPaging
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            UserMappingProfile();
            MembershipMappingProfile();
            GrowthRecordMappingProfile();
            PregnancyRecordMappingProfile();
        }
        private void UserMappingProfile()
        {
            CreateMap<User, LoginRequest>().ReverseMap();
            CreateMap<User, RegisterRequest>().ReverseMap();
            CreateMap<User, LoginGoogleRequest>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();            
        }

        private void MembershipMappingProfile()
        {
            CreateMap<Membership, MembershipResponse>().ReverseMap();
        }

        private void GrowthRecordMappingProfile()
        {
            CreateMap<GrowthRecord, GrowthRecordResponse>().ReverseMap();
        }
        private void PregnancyRecordMappingProfile()
        {
            CreateMap<PregnancyRecordRequest, PregnancyRecord>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.StartDate.ToDateTime(TimeOnly.MinValue))))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DueDate.ToDateTime(TimeOnly.MinValue))));

            CreateMap<PregnancyRecord, PregnancyRecordResponse>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate));
        }
    }
}
