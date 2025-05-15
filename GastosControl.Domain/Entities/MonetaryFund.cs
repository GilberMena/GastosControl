using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosControl.Domain.Entities
{
    public class MonetaryFund
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        public decimal Balance { get; set; } = 0;
    }
}
