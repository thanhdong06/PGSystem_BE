using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.ResponseModel
{
    public class FetusMeasurementResponse
    {
        public int MeasurementId { get; set; }
        public DateOnly DateMeasured { get; set; }
        public double Length { get; set; }
        public double HeadCircumference { get; set; }
        public double WeightEstimate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int FetusId { get; set; }

        public List<string> Warnings { get; set; } = new List<string>();

    }
}
