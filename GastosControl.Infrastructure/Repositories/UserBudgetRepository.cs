using GastosControl.Domain.Entities;
using GastosControl.Domain.Interfaces;
using GastosControl.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GastosControl.Infrastructure.Repositories
{
    public class UserBudgetRepository : IUserBudgetRepository
    {
        private readonly ApplicationDbContext _context;

        public UserBudgetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserBudget>> GetByUserIdAsync(int userId)
        {
            return await _context.UserBudgets
                .Where(b => b.UserId == userId)
                .Include(b => b.ExpenseType)
                .ToListAsync();
        }

        public async Task<UserBudget?> GetByIdAsync(int id)
        {
            return await _context.UserBudgets.FindAsync(id);
        }

        public async Task AddAsync(UserBudget budget)
        {
            await _context.UserBudgets.AddAsync(budget);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserBudget budget)
        {
            _context.UserBudgets.Update(budget);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.UserBudgets.FindAsync(id);
            if (entity != null)
            {
                _context.UserBudgets.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
