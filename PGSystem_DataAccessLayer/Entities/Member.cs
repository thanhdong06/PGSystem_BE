﻿using System;
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
        public int UserUID { get; set; }
        public User User { get; set; }

        // Liên kết với Membership
        public int MembershipID { get; set; }
        public Membership Membership { get; set; }

        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Reminder> Reminders { get; set; }
        public ICollection<PregnancyRecord> PregnancyRecord { get; set; }

        public bool IsDeleted { get; set; }
        public string Status { get; set; } = "Pending"; // Pending | Paid | Failed

        public int OrderCode { get; set; }
        public ICollection<TransactionEntity> Transactions { get; set; }

    }
}
