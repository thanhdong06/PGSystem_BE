using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.ResponseModel
{
    public class CommentResponse
    {
        public int CID { get; set; }
        public string Content { get; set; }
        public int BID { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int MemberID { get; set; }
        public UserComment User {  get; set; }
    }

    public class UserComment
    {
        public int UID { get; set; }
        public string FullName { get; set; }
    }
}
