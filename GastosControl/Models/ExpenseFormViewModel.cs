using GastosControl.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GastosControl.Models
{
    public class ExpenseFormViewModel
    {
        // Encabezado
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int MonetaryFundId { get; set; }

        public string Observation { get; set; } = string.Empty;
        public string CommerceName { get; set; } = string.Empty;

        [Required]
        public string DocumentType { get; set; } = "Factura";

        // Detalles
        public List<ExpenseDetailInput> Details { get; set; } = new();
    }

    public class ExpenseDetailInput
    {
        [Required]
        public int ExpenseTypeId { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Debe ingresar un valor positivo")]
        public decimal Amount { get; set; }
    }
}
