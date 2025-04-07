using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.Entities
{
    public class FetusMeasurement
    {
        [Key]
        public int MeasurementId { get; set; }

        public DateOnly DateMeasured { get; set; }
        public int Week {  get; set; }
        public double Length { get; set; }
        public double HeadCircumference { get; set; }
        public double WeightEstimate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int FetusId { get; set; }
        public Fetus Fetus { get; set; }
    }
}
