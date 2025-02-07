using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.Entities
{
    public class PregnancyRecord
    {
        public int PID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int MemberMemberID { get; set; }

        public Member Member { get; set; }
        public ICollection<Alert> Alerts { get; set; }
        public ICollection<GrowthRecord> GrowthRecords { get; set; }
        public ICollection<PregnancyGrowthReport> PregnancyGrowthReports { get; set; }
    }
}
