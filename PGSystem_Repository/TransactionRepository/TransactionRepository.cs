using Microsoft.EntityFrameworkCore;
using PGSystem_DataAccessLayer.DBContext;
using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.TransactionRepository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDBContext _context;

        public TransactionRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task AddTransactionAsync(TransactionEntity transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
        public async Task<TransactionEntity> GetByMemberIdAsync(int memberId)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.MemberID == memberId);
        }

        public async Task UpdateAsync(TransactionEntity transaction)
        {
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();
        }
        public async Task<List<TransactionEntity>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.Where(t=> t.Status == "Paid").ToListAsync();
        }
    }
}
