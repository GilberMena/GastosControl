using GastosControl.Application.DTOs;
using GastosControl.Domain.Interfaces;
using GastosControl.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosControl.Infrastructure.Repositories
{
    public class MovementRepository : IMovementRepository
    {
        private readonly ApplicationDbContext _context;

        public MovementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MovementDto>> GetMovementsAsync(DateTime from, DateTime to)
        {
            var depositos = await _context.Deposits
                .Where(d => d.Date >= from && d.Date <= to)
                .Include(d => d.MonetaryFund)
                .Include(d => d.User)
                .Select(d => new MovementDto
                {
                    User = d.UserId.ToString(), // si tienes d.User.Name, mejor
                    Type = "Depósito",
                    Date = d.Date,
                    Fund = d.MonetaryFund.Name,
                    FundDescription = d.MonetaryFund.Description,
                    Description = null,
                    Amount = d.Amount
                }).ToListAsync();

            var gastos = await _context.ExpenseHeaders
                .Where(h => h.Date >= from && h.Date <= to)
                .Include(h => h.MonetaryFund)
                .Include(h => h.User)
                .Include(h => h.Details)
                .Select(h => new MovementDto
                {
                    User = h.UserId.ToString(),
                    Type = "Gasto",
                    Date = h.Date,
                    Fund = h.MonetaryFund.Name,
                    FundDescription = h.MonetaryFund.Description,
                    Description = $"{h.CommerceName} ({h.Observation})",
                    Amount = h.Details.Sum(d => d.Amount)
                }).ToListAsync();

            return depositos.Concat(gastos).OrderByDescending(m => m.Date).ToList();
        }
    }

}
