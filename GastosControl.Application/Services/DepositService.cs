using GastosControl.Domain.Entities;

public class DepositService : IDepositService
{
    private readonly IDepositRepository _repository;

    public DepositService(IDepositRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Deposit>> GetByUserIdAsync(int userId)
        => _repository.GetByUserIdAsync(userId);

    public Task AddAsync(Deposit deposit)
        => _repository.AddAsync(deposit);

    public Task DeleteAsync(int id)
        => _repository.DeleteAsync(id);
}
