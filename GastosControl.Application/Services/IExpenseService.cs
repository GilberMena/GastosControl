using GastosControl.Domain.DTO;
using GastosControl.Application.DTOs;
using GastosControl.Domain.Entities;

namespace GastosControl.Application.Interfaces
{
    public interface IExpenseService
    {
        Task<List<ExpenseHeader>> GetByUserIdAsync(int userId);
        Task<ExpenseHeader?> GetByIdAsync(int id);
        Task AddAsync(ExpenseHeader header, List<ExpenseDetail> details);
        Task DeleteAsync(int id);
        Task<List<BudgetMovementDto>> GetMonthlyBudgetSummaryAsync(int userId, int month, int year);
        Task<List<TopGastoDto>> GetTop3GastosByUserAndMonth(int userId, int month, int year);
        Task<decimal> GetTotalDepositsAsync(int userId, int month, int year);
        Task<decimal> GetTotalExpensesAsync(int userId, int month, int year);

    }
}
