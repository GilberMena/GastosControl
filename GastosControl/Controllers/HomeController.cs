using GastosControl.Application.DTOs;
using GastosControl.Domain.DTO;
using GastosControl.Application.Interfaces;
using GastosControl.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace GastosControl.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExpenseService _expenseService;

        public HomeController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var now = DateTime.Now;
            var data = await _expenseService.GetMonthlyBudgetSummaryAsync(userId.Value, now.Month, now.Year);
            if (data == null)
                data = new List<BudgetMovementDto>();
            ViewBag.GraphData = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            });

            var top3 = await _expenseService.GetTop3GastosByUserAndMonth(userId.Value, now.Month, now.Year);
            ViewBag.Top3Gastos = top3;

            ViewBag.TotalDeposits = await _expenseService.GetTotalDepositsAsync(userId.Value, now.Month, now.Year);
            ViewBag.TotalExpenses = await _expenseService.GetTotalExpensesAsync(userId.Value, now.Month, now.Year);

            return View();
        }
    }

}
