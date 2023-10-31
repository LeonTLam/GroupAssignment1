using GroupAssignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using Transaction = GroupAssignment1.Models.Transaction;

namespace GroupAssignment1.DAL
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly HousingDbContext _db;
        private readonly ILogger<TransactionRepository> _logger;

        public TransactionRepository(HousingDbContext db, ILogger<TransactionRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IEnumerable<Transaction>?> GetAll()
        {
            try
            {
                return await _db.Transactions.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("[TransactionRepository] Transactions ToListAsync() failed when GetAll(), error message: {e}", e.Message);
                return null;
            }

        }

        public async Task<Transaction?> GetTransactionById(int id)
        {
            try
            {
                return await _db.Transactions.FindAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError("[TransactionRepository] Transaction FindAsync(id) failed when GetTransactionById for TransactionId {TransactionId:0000}, error messasge: {e}", id, e.Message);
                return null;
            }
        }

        public async Task<bool> Create(Transaction transaction)
        {
            try
            {
                _db.Transactions.Add(transaction);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[TransactionRepository] Transaction creation failed for item {@transaction}, error message: {e}", transaction, e.Message);
                return false;
            }
        }


    }
}
