using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.Entities
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberID { get; set; }

        // Liên kết với User
        public int UID { get; set; }
        public User User { get; set; }

        // Liên kết với Membership
        public int MembershipID { get; set; }
        public Membership Membership { get; set; }

        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Reminder> Reminders { get; set; }
        public PregnancyRecord PregnancyRecord { get; set; }

        public bool IsDeleted { get; set; }
    }
}
