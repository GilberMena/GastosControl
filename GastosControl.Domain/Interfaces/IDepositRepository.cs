using GastosControl.Domain.Entities;

public interface IDepositRepository
{
    Task<List<Deposit>> GetByUserIdAsync(int userId);
    Task AddAsync(Deposit deposit);
    Task DeleteAsync(int id);
}
