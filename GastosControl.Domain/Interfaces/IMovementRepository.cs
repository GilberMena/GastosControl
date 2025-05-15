using GastosControl.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosControl.Domain.Interfaces
{
    public interface IMovementRepository
    {
        Task<List<MovementDto>> GetMovementsAsync(DateTime from, DateTime to);
    }

}
