using GastosControl.Application.Interfaces;
using GastosControl.Domain.Entities;
using GastosControl.Domain.Interfaces;

namespace GastosControl.Application.Services
{
    public class MonetaryFundService : IMonetaryFundService
    {
        private readonly IMonetaryFundRepository _repository;

        public MonetaryFundService(IMonetaryFundRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MonetaryFund>> GetByUserIdAsync(int userId)
            => await _repository.GetByUserIdAsync(userId);

        public async Task<MonetaryFund?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task AddAsync(MonetaryFund fund)
            => await _repository.AddAsync(fund);

        public async Task UpdateAsync(MonetaryFund fund)
            => await _repository.UpdateAsync(fund);

        public async Task DeleteAsync(int id)
            => await _repository.DeleteAsync(id);
    }
}
