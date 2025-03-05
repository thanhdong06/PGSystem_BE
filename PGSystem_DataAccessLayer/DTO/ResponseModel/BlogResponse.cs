using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.ResponseModel
{
    public class BlogResponse
    {
        public int BID { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string? Type { get; set; }
        public int AID { get; set; }

    }
}
