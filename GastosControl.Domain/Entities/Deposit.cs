using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosControl.Domain.Entities
{
    public class Deposit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int MonetaryFundId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public User? User { get; set; }
        public MonetaryFund? MonetaryFund { get; set; } = null!;
    }
}
