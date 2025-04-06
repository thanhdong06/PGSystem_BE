using Microsoft.EntityFrameworkCore;
using PGSystem_DataAccessLayer.DBContext;
using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<List<Fetus>> GetByPregnancyRecordId(int pregnancyRecordId)
        {
            throw new NotImplementedException();
        }
    }
}
