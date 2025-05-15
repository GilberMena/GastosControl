using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosControl.Domain.Entities
{
    public class ExpenseDetail
    {
        public int Id { get; set; }
        public int HeaderId { get; set; }
        public int ExpenseTypeId { get; set; }
        public decimal Amount { get; set; }
        public ExpenseHeader Header { get; set; } = null!;
    }
}
