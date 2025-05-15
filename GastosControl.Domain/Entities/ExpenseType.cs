using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosControl.Domain.Entities
{
    public class ExpenseType
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int UserId { get; set; }


        public string Code => "ET" + Id.ToString("D4");
    }
}
