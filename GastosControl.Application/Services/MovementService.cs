using GastosControl.Application.DTOs;
using GastosControl.Application.Services;
using GastosControl.Domain.Interfaces;

public class MovementService : IMovementService
{
    private readonly IMovementRepository _repository;

    public MovementService(IMovementRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<MovementDto>> GetMovementsAsync(DateTime from, DateTime to)
    {
        return await _repository.GetMovementsAsync(from, to);
    }
}
