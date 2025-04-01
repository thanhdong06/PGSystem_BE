using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.ResponseModel
{
    public class LoginMemberResponse
    {
        public Member? Member { get; set; }
        public LoginResponse? LoginResponse { get; set; }
    }
}
