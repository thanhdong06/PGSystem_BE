using Microsoft.EntityFrameworkCore;
using PGSystem_DataAccessLayer.DBContext;
using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.ThresholdRepository
{
    public class ThresholdRepository : IThresholdRepository
    {
        private readonly AppDBContext _context;
        public ThresholdRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Thresholds> GetThresholdByNameAsync(string MeasurementType)
        {
            return await _context.Thresholds
                    .FirstOrDefaultAsync(t => t.MeasurementType == MeasurementType);
        }
    }
}
