using GastosControl.Domain.Entities;

namespace GastosControl.Application.Interfaces
{
    public interface IUserBudgetService
    {
        Task<List<UserBudget>> GetByUserIdAsync(int userId);
        Task<UserBudget?> GetByIdAsync(int id);
        Task AddAsync(UserBudget budget);
        Task UpdateAsync(UserBudget budget);
        Task DeleteAsync(int id);
    }
}
