using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.Entities
{
    public class Reminder
    {
        public int RID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int SID { get; set; }
        public int MemberID { get; set; }

        public RStatus RStatus { get; set; }
        public Member Member { get; set; }
        public bool IsDeleted { get; set; }
    }
}
