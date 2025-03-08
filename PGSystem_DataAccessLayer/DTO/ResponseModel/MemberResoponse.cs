using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.ResponseModel
{
    public class MemberResponse
    {

        public int MemberID { get; set; }
        public int UID { get; set; }
        public User FullName { get; set; }
        public string MembershipName { get; set; }
        
        public User Phone { get; set; }
        public User Email {  get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateAt { get; set; }
        


    }

    }
