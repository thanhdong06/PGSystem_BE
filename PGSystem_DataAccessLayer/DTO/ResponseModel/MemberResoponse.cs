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

        // Membership
        public int MembershipID { get; set; }
        public string MembershipName { get; set; }

        // Blogs
        public List<string> Blogs { get; set; }

        // Comments
        public List<string> Comments { get; set; }

        // Reminders
        public List<string> Reminders { get; set; }

        // Pregnancy Record (Nếu có)
        public string PregnancyRecord { get; set; }


    }
}