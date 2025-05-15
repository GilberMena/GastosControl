using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosControl.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public required string Identification { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string Login { get; set; }
        [Required]
        public required string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        [Required]
        public string Role { get; set; } = "Client";
    }
}
