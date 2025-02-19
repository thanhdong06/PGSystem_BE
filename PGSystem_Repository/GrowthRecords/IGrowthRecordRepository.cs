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
        Task AddAsync(GrowthRecord gr);
        Task<GrowthRecord?> GetGrowthRecordByGID(int id);
        Task<List<GrowthRecord>> ListGrowthRecordsByPID(int pid);
        Task<GrowthRecord> GetGrowthRecordByPidAndWeek(int pid, int week);
        Task<GrowthRecord> UpdateGrowthRecordAsync(GrowthRecord growthRecord);
        Task<bool> DeleteGrowthRecord(int id);
    }
}
