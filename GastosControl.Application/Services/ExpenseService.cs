using GastosControl.Domain.DTO;
using GastosControl.Application.DTOs;
using GastosControl.Application.Interfaces;
using GastosControl.Domain.Entities;
using GastosControl.Domain.Interfaces;
using System.Reflection.PortableExecutable;

namespace GastosControl.Application.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repository;
        private readonly IExpenseQueries _queries;

        public ExpenseService(IExpenseRepository repository, IExpenseQueries queries)
        {
            _repository = repository;
            _queries = queries;
        }

        public async Task<List<ExpenseHeader>> GetByUserIdAsync(int userId)
            => await _repository.GetByUserIdAsync(userId);

        public async Task<ExpenseHeader?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task AddAsync(ExpenseHeader header, List<ExpenseDetail> details)
        {
            if (details == null || details.Count == 0)
            {
                throw new InvalidOperationException("Debe registrar al menos un detalle del gasto.");
            }

            this.ValidateDuplicate(details);
            await this.ValidateExceededBudget(header, details);

            await _repository.AddAsync(header, details);
        }

        public async Task DeleteAsync(int id)
            => await _repository.DeleteAsync(id);

        public void ValidateDuplicate(List<ExpenseDetail> details)
        {
            var duplicados = details
                .GroupBy(d => d.ExpenseTypeId)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicados.Any())
            {
                throw new InvalidOperationException("No puedes registrar el mismo tipo de gasto más de una vez en los detalles.\n");
            }
        }
        public async Task ValidateExceededBudget(ExpenseHeader header, List<ExpenseDetail> details)
        {
            var month = header.Date.Month;
            var year = header.Date.Year;
            var userId = header.UserId;

            var alertas = new List<string>();

            foreach (var detalle in details)
            {
                var presupuesto = await _queries.GetUserBudgetAsync(userId, detalle.ExpenseTypeId, month, year);

                if (presupuesto != null)
                {
                    var ejecutado = await _queries.GetExecutedAmountAsync(userId, detalle.ExpenseTypeId, month, year);

                    var totalProyectado = ejecutado + detalle.Amount;

                    if (totalProyectado > presupuesto.Amount)
                    {
                        var exceso = totalProyectado - presupuesto.Amount;
                        alertas.Add($"Tipo de gasto '{detalle.ExpenseTypeId}' excede el presupuesto de {presupuesto.Amount}. Se excede por {exceso}.\n");
                    }
                }
            }


            var totalDelGasto = details.Sum(d => d.Amount);

            var balance = await _queries.GetBalanceAsync(header.MonetaryFundId);
            if (balance < 0)
            {
                alertas.Add("Fondo monetario no encontrado.\n");
            }
            else if (totalDelGasto > balance)
            {
                var diferencia = totalDelGasto - balance;
                alertas.Add($"El total del gasto ({totalDelGasto}) excede el saldo disponible del fondo ({balance}). Faltan {diferencia}.\n");
            }

            if (alertas.Any())
            {
                throw new InvalidOperationException(string.Join("\n", alertas));
            }

        }

        public async Task<List<BudgetMovementDto>> GetMonthlyBudgetSummaryAsync(int userId, int month, int year)
        {
            var typeExpense = await _queries.GetExpenseTypesAsync(userId);
            var resume = new List<BudgetMovementDto>();

            foreach (var type in typeExpense)
            {
                var budget = await _queries.GetUserBudgetAsync(userId, type.Id, month, year);
                var execute = await _queries.GetExecutedAmountAsync(userId, type.Id, month, year);

                resume.Add(new BudgetMovementDto
                {
                    ExpenseTypeName = type.Name,
                    Budget = budget?.Amount ?? 0,
                    Executed = execute
                });
            }

            return resume;
        }

        public async Task<List<TopGastoDto>> GetTop3GastosByUserAndMonth(int userId, int month, int year)
        {
            return await _queries.GetTop3GastosByUserAndMonth(userId, month, year);
        }

        public async Task<decimal> GetTotalDepositsAsync(int userId, int month, int year)
        {
            return await _queries.GetTotalDepositsAsync(userId, month, year);
        }

        public async Task<decimal> GetTotalExpensesAsync(int userId, int month, int year)
        {
            return await _queries.GetTotalExpensesAsync(userId, month, year);
        }

    }
}
