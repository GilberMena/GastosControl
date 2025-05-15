using GastosControl.Application.Services;
using Microsoft.AspNetCore.Mvc;

public class MovementController : Controller
{
    private readonly IMovementService _service;

    public MovementController(IMovementService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Result(DateTime from, DateTime to)
    {
        to = to.Date.AddDays(1).AddTicks(-1);

        var movimientos = await _service.GetMovementsAsync(from, to);
        ViewBag.From = from.ToShortDateString();
        ViewBag.To = to.ToShortDateString();
        return View(movimientos);
    }
}
