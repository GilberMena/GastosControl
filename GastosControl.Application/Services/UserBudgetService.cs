using GastosControl.Application.Interfaces;
using GastosControl.Domain.Entities;
using GastosControl.Domain.Interfaces;

namespace GastosControl.Application.Services
{
    public class UserBudgetService : IUserBudgetService
    {
        private readonly IUserBudgetRepository _repository;

        public UserBudgetService(IUserBudgetRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserBudget>> GetByUserIdAsync(int userId)
            => await _repository.GetByUserIdAsync(userId);

        public async Task<UserBudget?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task AddAsync(UserBudget budget)
            => await _repository.AddAsync(budget);

        public async Task UpdateAsync(UserBudget budget)
            => await _repository.UpdateAsync(budget);

        public async Task DeleteAsync(int id)
            => await _repository.DeleteAsync(id);
    }
}
