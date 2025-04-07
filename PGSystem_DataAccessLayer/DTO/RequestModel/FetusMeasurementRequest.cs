using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.RequestModel
{
    public class FetusMeasurementRequest
    {
        public double Length { get; set; }
        public double HeadCircumference { get; set; }
        public double WeightEstimate { get; set; }
        public int Week { get; set; }
    }
}
