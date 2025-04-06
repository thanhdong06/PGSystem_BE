using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.Entities
{
    public class Fetus
    {
        [Key]
        public int FetusId { get; set; }

        public string Nickname { get; set; }  // Tuỳ chọn: "Bé A", "Bé B",...

        public int PregnancyRecordId { get; set; }
        public PregnancyRecord PregnancyRecord { get; set; }

        public ICollection<FetusMeasurement> Measurements { get; set; } = new List<FetusMeasurement>();
    }
}
