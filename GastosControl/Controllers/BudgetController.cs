using GastosControl.Application.Interfaces;
using GastosControl.Application.Services;
using GastosControl.Domain.Entities;
using GastosControl.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace GastosControl.Controllers
{
    public class BudgetController : Controller
    {
        private readonly IUserBudgetService _repository;
        private readonly IExpenseService _expenseService;
        private readonly ApplicationDbContext _context;

        public BudgetController(IExpenseService expenseService, IUserBudgetService repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
            _expenseService = expenseService;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Summary(string BudgetMonth)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var date = BudgetMonth.Split('-');
            var year = int.Parse(date[0]);
            var month = int.Parse(date[1]);

            var resumen = await _expenseService.GetMonthlyBudgetSummaryAsync(userId.Value, month, year);
            ViewBag.Month = month;
            ViewBag.Year = year;
            return View(resumen);
        }
    }

}
