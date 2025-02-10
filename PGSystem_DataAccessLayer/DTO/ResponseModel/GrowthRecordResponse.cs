using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.ResponseModel
{
    public class GrowthRecordResponse
    {
        public int PID { get; set; }
        public int Week { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
