using GastosControl.Domain.Entities;

public interface IDepositService
{
    Task<List<Deposit>> GetByUserIdAsync(int userId);
    Task AddAsync(Deposit deposit);
    Task DeleteAsync(int id);
}
