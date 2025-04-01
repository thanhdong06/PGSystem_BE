using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.RequestModel
{
    public class CommentRequest
    {
        public string Content { get; set; }
        public int BID { get; set; }
        public int MemberID { get; set; }
    }
}
