using Microsoft.EntityFrameworkCore;
using PGSystem_DataAccessLayer.DBContext;
using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.PregnancyRecords
{
    public class PregnancyRecordRepository : IPregnancyRecordRepository
    {
        private readonly AppDBContext _context;

        public PregnancyRecordRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<PregnancyRecord> AddAsync(PregnancyRecord entity)
        {
            _context.PregnancyRecords.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<PregnancyRecord>> GetPregnancyRecordByMemberIDAsync(int memberId)
        {
            return await _context.PregnancyRecords
                .Include(r => r.Fetuses)
                .Where(r => r.MemberMemberID == memberId)
                .ToListAsync();
        }
        public async Task<PregnancyRecord?> GetByIdAsync(int id)
        {
            return await _context.PregnancyRecords.FirstOrDefaultAsync(p => p.PID == id);
        }
        public async Task UpdateAsync(PregnancyRecord pregnancyRecord)
        {
            _context.PregnancyRecords.Update(pregnancyRecord);
            await _context.SaveChangesAsync();
        }

        public void Delete(List<PregnancyRecord> pregnancyRecords)
        {
            _context.PregnancyRecords.RemoveRange(pregnancyRecords);
        }

    }
}
