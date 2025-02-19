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
        public async Task AddAsync(GrowthRecord gr)
        {
            _context.GrowthRecords.Add(gr);
            await _context.SaveChangesAsync();
        }
        public async Task<GrowthRecord?> GetGrowthRecordByGID(int id)
        {
            return await _context.GrowthRecords
                .FirstOrDefaultAsync(gr => gr.GID == id && !gr.IsDeleted);
        }
        public async Task<List<GrowthRecord>> ListGrowthRecordsByPID(int pid)
        {
            return await _context.GrowthRecords
                            .Where(gr => gr.PID == pid && !gr.IsDeleted)
                            .ToListAsync();
        }
        public async Task<GrowthRecord> GetGrowthRecordByPidAndWeek(int pid, int week)
        {
            return await _context.GrowthRecords.FirstOrDefaultAsync(gr => gr.PID == pid && gr.Week == week && !gr.IsDeleted);
        }
        public async Task<GrowthRecord> UpdateGrowthRecordAsync(GrowthRecord growthRecord)
        {
            _context.GrowthRecords.Update(growthRecord);
            await _context.SaveChangesAsync();
            return growthRecord;
        }
        public async Task<bool> DeleteGrowthRecord(int id)
        {
            var growthRecord = await _context.GrowthRecords.FirstOrDefaultAsync(gr => gr.GID == id && !gr.IsDeleted);

            if (growthRecord == null)
            {
                return false;
            }
            growthRecord.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<List<GrowthRecord>> GetGrowthRecordsForChart(int memberId)
        {
            return await _context.GrowthRecords
                .Where(gr => gr.PregnancyRecord.MemberMemberID == memberId && !gr.IsDeleted)
                .OrderBy(gr => gr.Week)
                .ToListAsync();
        }
    }
}
