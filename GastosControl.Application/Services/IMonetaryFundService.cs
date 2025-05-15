using GastosControl.Domain.Entities;

namespace GastosControl.Application.Interfaces
{
    public interface IMonetaryFundService
    {
        Task<List<MonetaryFund>> GetByUserIdAsync(int userId);
        Task<MonetaryFund?> GetByIdAsync(int id);
        Task AddAsync(MonetaryFund fund);
        Task UpdateAsync(MonetaryFund fund);
        Task DeleteAsync(int id);
    }
}
