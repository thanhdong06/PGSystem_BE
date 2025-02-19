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
    }
}
