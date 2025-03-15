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
            ReminderMappingProfile();
            BlogMappingProfile();
            CommentMappingProfile();
            MemberMappingProfile();
            MemberResponseProfile();
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
            CreateMap<Membership, MembershipsRequest>().ReverseMap();
        }

        private void GrowthRecordMappingProfile()
        {
            CreateMap<GrowthRecord, GrowthRecordResponse>().ReverseMap();
            CreateMap<GrowthRecordRequest, GrowthRecord>()
                       .ForMember(dest => dest.GID, opt => opt.Ignore())
                       .ForMember(dest => dest.PregnancyRecord, opt => opt.Ignore());        
        }
        private void PregnancyRecordMappingProfile()
        {
            CreateMap<PregnancyRecordRequest, PregnancyRecord>();
               
            CreateMap<PregnancyRecord, PregnancyRecordResponse>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate));
        }
        private void ReminderMappingProfile()
        {
            CreateMap<ReminderRequest, Reminder>();
            CreateMap<Reminder, ReminderResponse>();
        }
        public void BlogMappingProfile()
        {
            CreateMap<User, UserBlog>();
            CreateMap<Blog, BlogResponse>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Member.User));
        }
        public void CommentMappingProfile()
        {
            CreateMap<User, UserComment>();
            CreateMap<Comment, CommentResponse>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Member.User));
        }
        private void MemberMappingProfile()
        {
            CreateMap<Member, MemberResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserUID))
                .ForMember(dest => dest.MembershipId, opt => opt.MapFrom(src => src.MembershipID))
                .ReverseMap();

            CreateMap<RegisterMembershipRequest, Member>()
                .ForMember(dest => dest.MemberID, opt => opt.Ignore()) // ID tự động tạo
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore()) // Mặc định là false
                .ForMember(dest => dest.User, opt => opt.Ignore()) // Không map User object
                .ForMember(dest => dest.Membership, opt => opt.Ignore()); // Không map Membership object
            CreateMap<Member, MemberResponse>()
          .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
          
          .ForMember(dest => dest.MembershipName, opt => opt.MapFrom(src => src.Membership.Name));
        }

        private void MemberResponseProfile()
        {

            CreateMap<Member, MemberResponse>();
            CreateMap<MemberShipUpdateRequest, Member>()
                 .ForMember(dest => dest.UserUID, opt => opt.MapFrom(src => src.UserUID))
            .ForMember(dest => dest.MembershipID, opt => opt.MapFrom(src => src.NewMembershipID));

            // Mapping từ Member sang MemberResponse
            CreateMap<Member, MemberResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserUID));

        }
    }
}
