using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.TransactionRepository
{
    public interface ITransactionRepository
    {
        Task AddTransactionAsync(TransactionEntity transaction);
        Task<TransactionEntity> GetByMemberIdAsync(int memberId);
        Task UpdateAsync(TransactionEntity transaction);
        Task<List<TransactionEntity>> GetAllTransactionsAsync();
    }
}
