using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.Entities
{
    public class PregnancyRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PID { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly DueDate { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string Status { get; set; }
        public int MemberMemberID { get; set; }

        public Member Member { get; set; }
        public ICollection<Alert> Alerts { get; set; }
        public ICollection<GrowthRecord> GrowthRecords { get; set; } = new List<GrowthRecord>();
        public ICollection<PregnancyGrowthReport> PregnancyGrowthReports { get; set; }
        public ICollection<Fetus> Fetuses { get; set; } = new List<Fetus>();

    }
}
