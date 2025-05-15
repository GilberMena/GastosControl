using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosControl.Domain.Entities
{
    public class ExpenseHeader
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int MonetaryFundId { get; set; }
        public required string Observation { get; set; }
        public required string CommerceName { get; set; }
        public required string DocumentType { get; set; }
        public MonetaryFund? MonetaryFund { get; set; }
        public User? User { get; set; }

        public List<ExpenseDetail> Details { get; set; } = new();
    }
}
