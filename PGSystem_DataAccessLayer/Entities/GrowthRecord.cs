using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.Entities
{
    public class GrowthRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GID { get; set; }
        public int PID { get; set; }
        public int Week { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool IsDeleted { get; set; }
        public PregnancyRecord PregnancyRecord { get; set; }
    }
}
