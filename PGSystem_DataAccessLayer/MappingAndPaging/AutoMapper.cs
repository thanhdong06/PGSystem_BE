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
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            UserMappingProfile();
        }
        private void UserMappingProfile()
        {
            CreateMap<User, LoginRequest>().ReverseMap();
            CreateMap<User, ResponseUser>().ReverseMap();

        }
    }
}
