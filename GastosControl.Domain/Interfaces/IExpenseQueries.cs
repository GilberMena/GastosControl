using GastosControl.Domain.DTO;
using GastosControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosControl.Domain.Interfaces
{
    public interface IExpenseQueries
    {
        Task<UserBudget?> GetUserBudgetAsync(int userId, int expenseTypeId, int month, int year);
        Task<decimal> GetExecutedAmountAsync(int userId, int expenseTypeId, int month, int year);
        Task<decimal> GetBalanceAsync(int monetaryFundId);
        Task<List<ExpenseType>> GetExpenseTypesAsync(int userId);
        Task<List<TopGastoDto>> GetTop3GastosByUserAndMonth(int userId, int month, int year);
        Task<decimal> GetTotalDepositsAsync(int userId, int month, int year);
        Task<decimal> GetTotalExpensesAsync(int userId, int month, int year);
    }
}
