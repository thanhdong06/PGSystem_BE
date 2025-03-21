using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.Entities
{
    public class TransactionEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }

        // Liên kết với Member
        public int MemberID { get; set; }
        public Member Member { get; set; }

        // Giao dịch thông tin
        public decimal Amount { get; set; } 
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        public string Status { get; set; }  // "Success", "Pending", "Failed"

    }
}
