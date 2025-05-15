using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosControl.Application.DTOs
{
    public class MovementDto
    {
        public string User { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // "Depósito" o "Gasto"
        public DateTime Date { get; set; }
        public string Fund { get; set; } = string.Empty;
        public string FundDescription { get; set; } = string.Empty;
        public string? Description { get; set; } // Observación o Comercio
        public decimal Amount { get; set; }
    }

}
