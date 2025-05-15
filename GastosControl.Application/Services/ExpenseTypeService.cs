using GastosControl.Domain.Entities;
using GastosControl.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosControl.Application.Services
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly IExpenseTypeRepository _repository;

        public ExpenseTypeService(IExpenseTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ExpenseType>> GetAllAsync(int userId)
            => await _repository.GetAllAsync(userId);

        public async Task<ExpenseType?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task AddAsync(ExpenseType expenseType)
            => await _repository.AddAsync(expenseType);

        public async Task UpdateAsync(ExpenseType expenseType)
            => await _repository.UpdateAsync(expenseType);

        public async Task DeleteAsync(int id)
            => await _repository.DeleteAsync(id);
    }
}
