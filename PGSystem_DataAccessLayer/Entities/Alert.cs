using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.Entities
{
    public class Alert
    {
        public int AlertID { get; set; }
        public int PID { get; set; }
        public int Week { get; set; }
        public string Message { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public PregnancyRecord PregnancyRecord { get; set; }

    }
}
