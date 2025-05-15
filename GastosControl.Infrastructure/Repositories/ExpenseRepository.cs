using GastosControl.Domain.DTO;
using GastosControl.Domain.Entities;
using GastosControl.Domain.Interfaces;
using GastosControl.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using static System.Net.Mime.MediaTypeNames;

namespace GastosControl.Infrastructure.Repositories
{
    public class ExpenseRepository : IExpenseRepository, IExpenseQueries
    {
        private readonly ApplicationDbContext _context;

        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExpenseHeader>> GetByUserIdAsync(int userId)
        {
            return await _context.ExpenseHeaders
                .Where(e => e.UserId == userId)
                .Include(e => e.MonetaryFund)
                .Include(e => e.Details)
                .ToListAsync();
        }

        public async Task<ExpenseHeader?> GetByIdAsync(int id)
        {
            return await _context.ExpenseHeaders
                .Include(e => e.Details)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(ExpenseHeader header, List<ExpenseDetail> details)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.ExpenseHeaders.AddAsync(header);
                await _context.SaveChangesAsync();

                foreach (var detail in details)
                {
                    detail.HeaderId = header.Id;
                    await _context.ExpenseDetails.AddAsync(detail);
                }

                await _context.SaveChangesAsync();

                var totalExpense = details.Sum(d => d.Amount);
                var fondo = await _context.MonetaryFunds.FindAsync(header.MonetaryFundId);
                if (fondo != null)
                {
                    fondo.Balance -= totalExpense;
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var header = await _context.ExpenseHeaders.Include(h => h.Details).FirstOrDefaultAsync(h => h.Id == id);
            if (header != null)
            {
                _context.ExpenseDetails.RemoveRange(header.Details);
                _context.ExpenseHeaders.Remove(header);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserBudget?> GetUserBudgetAsync(int userId, int expenseTypeId, int month, int year)
        {
            return await _context.UserBudgets.FirstOrDefaultAsync(p =>
                p.UserId == userId &&
                p.ExpenseTypeId == expenseTypeId &&
                p.Month == month &&
                p.Year == year);
        }

        public async Task<decimal> GetExecutedAmountAsync(int userId, int expenseTypeId, int month, int year)
        {
            return await _context.ExpenseDetails
                .Include(d => d.Header)
                .Where(d => d.ExpenseTypeId == expenseTypeId &&
                            d.Header.UserId == userId &&
                            d.Header.Date.Month == month &&
                            d.Header.Date.Year == year)
                .SumAsync(d => d.Amount);
        }

        public async Task<decimal> GetBalanceAsync(int monetaryFundId)
        {
            var fondo = await _context.MonetaryFunds.FindAsync(monetaryFundId);
            if (fondo is null)
            {
                return -1;
            }
            return fondo.Balance;
        }
        public async Task<List<ExpenseType>> GetExpenseTypesAsync(int userId)
        {
            return await _context.ExpenseTypes
                .Where(et => et.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<TopGastoDto>> GetTop3GastosByUserAndMonth(int userId, int month, int year)
        {
            return await _context.ExpenseDetails
                .Where(d => d.Header.UserId == userId &&
                            d.Header.Date.Month == month &&
                            d.Header.Date.Year == year)
                .GroupBy(d => d.ExpenseTypeId)
                .Select(g => new
                {
                    ExpenseTypeId = g.Key,
                    Total = g.Sum(d => d.Amount)
                })
                .OrderByDescending(g => g.Total)
                .Take(3)
                .Join(_context.ExpenseTypes,
                      g => g.ExpenseTypeId,
                      t => t.Id,
                      (g, t) => new TopGastoDto
                      {
                          Name = t.Name,
                          Amount = g.Total
                      })
                .ToListAsync();
        }

        public async Task<decimal> GetTotalDepositsAsync(int userId, int month, int year)
        {
            return await _context.Deposits
                .Where(d => d.UserId == userId &&
                            d.Date.Month == month &&
                            d.Date.Year == year)
                .SumAsync(d => d.Amount);
        }

        public async Task<decimal> GetTotalExpensesAsync(int userId, int month, int year)
        {
            return await _context.ExpenseHeaders
                .Where(h => h.UserId == userId &&
                            h.Date.Month == month &&
                            h.Date.Year == year)
                .SelectMany(h => h.Details)
                .SumAsync(d => d.Amount);
        }
    }
}
