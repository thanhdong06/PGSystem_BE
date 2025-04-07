using Microsoft.EntityFrameworkCore;
using PGSystem_DataAccessLayer.DBContext;
using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.Fetuss
{
    public class FetusRepository : IFetusRepository
    {
        private readonly AppDBContext _context;
        public FetusRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Fetus> AddAsync(Fetus fetus)
        {
            _context.Fetuses.Add(fetus);
            await _context.SaveChangesAsync();
            return fetus;
        }

        public async Task<List<Fetus>> GetFetusByPregnancyRecordId(int pregnancyRecordId)
        {
            return await _context.Fetuses.Where(f => f.PregnancyRecordId == pregnancyRecordId).ToListAsync();

        }
        public async Task<Fetus> GetFetusWithPregnancyAsync(int fetusId)
        {
            return await _context.Fetuses
                .Include(f => f.PregnancyRecord)
                .FirstOrDefaultAsync(f => f.FetusId == fetusId);
        }
        public async Task<bool> ExistsAsync(Expression<Func<Fetus, bool>> predicate)
        {
            return await _context.Fetuses.AnyAsync(predicate);
        } 
        public async Task<bool> ExistsDateAsync(Expression<Func<FetusMeasurement, bool>> predicate)
        {
            return await _context.FetusMeasurements.AnyAsync(predicate);
        }
        public async Task<FetusMeasurement> AddMeasurementAsync(FetusMeasurement fetus)
        {
            _context.FetusMeasurements.Add(fetus);
            await _context.SaveChangesAsync();
            return fetus;
        }

        public async Task<FetusMeasurement> GetMeasurementByIdAsync(int measurementId)
        {
            return await _context.FetusMeasurements.Where(f => f.MeasurementId == measurementId).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(FetusMeasurement fetusMeasurement)
        {
            _context.FetusMeasurements.Update(fetusMeasurement);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FetusMeasurement>> GetAllMeasurementByFetusId(int fetusId)
        {
            return await _context.FetusMeasurements.Where(f => f.FetusId == fetusId).ToListAsync();

        }
        public async Task<FetusMeasurement?> GetLatestBeforeWeekAsync(int fetusId, int currentWeek, int excludeMeasurementId)
        {
            return await _context.FetusMeasurements
                .Where(m => m.FetusId == fetusId && m.Week < currentWeek && m.MeasurementId != excludeMeasurementId)
                .OrderByDescending(m => m.Week)
                .FirstOrDefaultAsync();
        }
        public async Task<FetusMeasurement?> GetPreviousMeasurementAsync(int fetusId, int currentWeek)
        {
            return await _context.FetusMeasurements
                .Where(m => m.FetusId == fetusId && m.Week < currentWeek)
                .OrderByDescending(m => m.DateMeasured)
                .FirstOrDefaultAsync();
        }
    }
}
