using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.Entities
{
    public class Member
    {
        public int MemberID { get; set; }
        public int MembershipID { get; set; } 
        public string MemberName { get; set; }
        public Membership Membership { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Reminder> Reminders { get; set; }
        public PregnancyRecord PregnancyRecord { get; set; }
    }
}
