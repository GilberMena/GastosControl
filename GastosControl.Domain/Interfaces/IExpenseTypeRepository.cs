﻿using GastosControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosControl.Domain.Interfaces
{
    public interface IExpenseTypeRepository
    {
        Task<List<ExpenseType>> GetAllAsync(int userId);
        Task<ExpenseType?> GetByIdAsync(int id);
        Task AddAsync(ExpenseType expenseType);
        Task UpdateAsync(ExpenseType expenseType);
        Task DeleteAsync(int id);
    }
}
