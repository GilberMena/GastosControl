using GastosControl.Application.Interfaces;
using GastosControl.Application.Services;
using GastosControl.Domain.Entities;
using GastosControl.Helpers;
using GastosControl.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GastosControl.Controllers
{
    [AuthorizeSession]
    public class UserBudgetController : Controller
    {
        private readonly IUserBudgetService _repository;
        private readonly ApplicationDbContext _context;

        public UserBudgetController(IUserBudgetService repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        // GET: UserBudget
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");
            return View(await _repository.GetByUserIdAsync((int)userId));
        }

        // GET: UserBudget/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBudget = await _context.UserBudgets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userBudget == null)
            {
                return NotFound();
            }

            return View(userBudget);
        }

        // GET: UserBudget/Create
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");
            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes.Where(f => f.UserId == (int)userId), "Id", "Name");

            return View();
        }

        // POST: UserBudget/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExpenseTypeId,Amount")] UserBudget userBudget, string BudgetMonth)
        {
            userBudget.UserId = (int)HttpContext.Session.GetInt32("UserId")!;

            var date = BudgetMonth.Split('-');
            userBudget.Year = int.Parse(date[0]);
            userBudget.Month = int.Parse(date[1]);

            if (ModelState.IsValid)
            {
                _context.Add(userBudget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userBudget);
        }

        // GET: UserBudget/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBudget = await _context.UserBudgets.FindAsync(id);
            if (userBudget == null)
            {
                return NotFound();
            }

            ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "Id", "Name");

            return View(userBudget);
        }

        // POST: UserBudget/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExpenseTypeId,Month,Year,Amount")] UserBudget userBudget)
        {
            if (id != userBudget.Id)
            {
                return NotFound();
            }
            userBudget.UserId = (int)HttpContext.Session.GetInt32("UserId")!;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userBudget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserBudgetExists(userBudget.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userBudget);
        }

        // GET: UserBudget/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBudget = await _context.UserBudgets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userBudget == null)
            {
                return NotFound();
            }

            return View(userBudget);
        }

        // POST: UserBudget/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userBudget = await _context.UserBudgets.FindAsync(id);
            if (userBudget != null)
            {
                _context.UserBudgets.Remove(userBudget);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserBudgetExists(int id)
        {
            return _context.UserBudgets.Any(e => e.Id == id);
        }
    }
}
