using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.Entities
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CID { get; set; }
        public string Content { get; set; }
        public int BID { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int MemberID { get; set; }

        [ForeignKey("BID")]
        public Blog Blog { get; set; }

        [ForeignKey("MemberID")]
        public Member Member { get; set; }
        public bool IsDeleted { get; set; }
    }
}
