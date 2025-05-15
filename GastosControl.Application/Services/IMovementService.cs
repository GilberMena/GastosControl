using GastosControl.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosControl.Application.Services
{
    public interface IMovementService
    {
        Task<List<MovementDto>> GetMovementsAsync(DateTime from, DateTime to);
    }
}
