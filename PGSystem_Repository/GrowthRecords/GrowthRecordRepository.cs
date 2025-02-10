using Microsoft.EntityFrameworkCore;
using PGSystem_DataAccessLayer.DBContext;
using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.GrowthRecords
{
    public class GrowthRecordRepository : IGrowthRecordRepository
    {
        private readonly AppDBContext _context;

        public GrowthRecordRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<GrowthRecord> GetGrowthRecordByPidAsync(int pid, int week)
        {
            return await _context.GrowthRecords.FirstOrDefaultAsync(gr => gr.PID == pid && gr.Week == week);
        }
        public async Task<GrowthRecord> UpdateGrowthRecordAsync(GrowthRecord growthRecord)
        {
            _context.GrowthRecords.Update(growthRecord);
            await _context.SaveChangesAsync();
            return growthRecord;
        }
    }
}
