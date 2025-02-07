using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.Entities
{
    public class Comment
    {
        public int CID { get; set; }
        public string Content { get; set; }
        public int BID { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public Blog Blog { get; set; }
    }
}
