using GastosControl.Domain.Entities;
using GastosControl.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

public class DepositRepository : IDepositRepository
{
    private readonly ApplicationDbContext _context;

    public DepositRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Deposit>> GetByUserIdAsync(int userId)
    {
        return await _context.Deposits
            .Where(d => d.UserId == userId)
            .Include(d => d.MonetaryFund)
            .ToListAsync();
    }

    public async Task AddAsync(Deposit deposit)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            await _context.Deposits.AddAsync(deposit);
            await _context.SaveChangesAsync();

            var fondo = await _context.MonetaryFunds.FindAsync(deposit.MonetaryFundId);
            if (fondo != null)
            {
                fondo.Balance += deposit.Amount;
                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var deposit = await _context.Deposits.FindAsync(id);
        if (deposit != null)
        {
            _context.Deposits.Remove(deposit);
            await _context.SaveChangesAsync();
        }
    }
}
