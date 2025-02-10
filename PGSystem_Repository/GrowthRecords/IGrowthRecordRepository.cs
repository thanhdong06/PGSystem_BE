using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.GrowthRecords
{
    public interface IGrowthRecordRepository
    {
        Task<GrowthRecord> GetGrowthRecordByPidAsync(int pid, int week);
        Task<GrowthRecord> UpdateGrowthRecordAsync(GrowthRecord growthRecord);
    }
}
