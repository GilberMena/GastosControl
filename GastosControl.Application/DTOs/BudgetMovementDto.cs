using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosControl.Application.DTOs
{
    public class BudgetMovementDto
    {
        public string ExpenseTypeName { get; set; } = string.Empty;
        public decimal Budget { get; set; }
        public decimal Executed { get; set; }
        public decimal Available => Budget - Executed;
    }

}
