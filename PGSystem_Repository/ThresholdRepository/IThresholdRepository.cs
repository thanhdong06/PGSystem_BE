using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.ThresholdRepository
{
    public interface IThresholdRepository
    {
        Task<Thresholds> GetThresholdByNameAsync(string MeasurementType, int week);
    }
}
