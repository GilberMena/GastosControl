using GastosControl.Domain.Entities;
using GastosControl.Domain.Interfaces;
using GastosControl.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GastosControl.Infrastructure.Repositories
{
    public class MonetaryFundRepository : IMonetaryFundRepository
    {
        private readonly ApplicationDbContext _context;

        public MonetaryFundRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MonetaryFund>> GetByUserIdAsync(int userId)
        {
            return await _context.MonetaryFunds
                                 .Where(f => f.UserId == userId)
                                 .ToListAsync();
        }

        public async Task<MonetaryFund?> GetByIdAsync(int id)
        {
            return await _context.MonetaryFunds.FindAsync(id);
        }

        public async Task AddAsync(MonetaryFund fund)
        {
            await _context.MonetaryFunds.AddAsync(fund);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MonetaryFund fund)
        {
            _context.MonetaryFunds.Update(fund);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.MonetaryFunds.FindAsync(id);
            if (entity != null)
            {
                _context.MonetaryFunds.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
