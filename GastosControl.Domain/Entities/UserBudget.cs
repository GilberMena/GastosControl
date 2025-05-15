using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosControl.Domain.Entities
{
    public class UserBudget
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ExpenseTypeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public ExpenseType? ExpenseType { get; set; } = null!;
    }
}
