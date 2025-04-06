using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.Entities
{
    public class Thresholds
    {
        [Key]
        public int ThresholdsId { get; set; }

        public string MeasurementType { get; set; }
        public int Week { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public string WarningMessage { get; set; }

    }
}
