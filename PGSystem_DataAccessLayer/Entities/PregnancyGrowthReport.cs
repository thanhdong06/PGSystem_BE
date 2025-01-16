using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.Entities
{
    public class PregnancyGrowthReport
    {
        public int PGID { get; set; }
        public int PID { get; set; }
        public int Week { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string? Message { get; set; }
        public PregnancyRecord PregnancyRecord { get; set; }
    }
}
