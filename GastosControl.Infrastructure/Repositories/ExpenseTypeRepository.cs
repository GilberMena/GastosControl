using GastosControl.Domain.Entities;
using GastosControl.Domain.Interfaces;
using GastosControl.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosControl.Infrastructure.Repositories
{
    public class ExpenseTypeRepository : IExpenseTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenseTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExpenseType>> GetAllAsync(int userId)
        {
            return await _context.ExpenseTypes
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }

        public async Task<ExpenseType?> GetByIdAsync(int id)
        {
            return await _context.ExpenseTypes.FindAsync(id);
        }

        public async Task AddAsync(ExpenseType expenseType)
        {
            await _context.ExpenseTypes.AddAsync(expenseType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ExpenseType expenseType)
        {
            _context.ExpenseTypes.Update(expenseType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ExpenseTypes.FindAsync(id);
            if (entity != null)
            {
                _context.ExpenseTypes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
