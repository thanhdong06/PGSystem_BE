using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.RequestModel
{
    public class RegisterMembershipRequest
    {
        public int UserId { get; set; }
        public int MembershipId { get; set; }
    }
}
