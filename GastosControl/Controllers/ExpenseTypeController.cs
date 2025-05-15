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
    public class ExpenseTypeController : Controller
    {
        private readonly IExpenseTypeService _repository;
        private readonly ApplicationDbContext _context;

        public ExpenseTypeController(IExpenseTypeService repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        // GET: ExpenseType
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");
            return View(await _repository.GetAllAsync((int)userId));
        }

        // GET: ExpenseType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseType = await _context.ExpenseTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseType == null)
            {
                return NotFound();
            }

            return View(expenseType);
        }

        // GET: ExpenseType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExpenseType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ExpenseType expenseType)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");

            if (ModelState.IsValid)
            {
                expenseType.UserId = userId.Value;
                _context.Add(expenseType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenseType);
        }

        // GET: ExpenseType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseType = await _context.ExpenseTypes.FindAsync(id);
            if (expenseType == null)
            {
                return NotFound();
            }
            return View(expenseType);
        }

        // POST: ExpenseType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ExpenseType expenseType)
        {
            if (id != expenseType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenseType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseTypeExists(expenseType.Id))
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
            return View(expenseType);
        }

        // GET: ExpenseType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseType = await _context.ExpenseTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseType == null)
            {
                return NotFound();
            }

            return View(expenseType);
        }

        // POST: ExpenseType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expenseType = await _context.ExpenseTypes.FindAsync(id);
            if (expenseType != null)
            {
                _context.ExpenseTypes.Remove(expenseType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseTypeExists(int id)
        {
            return _context.ExpenseTypes.Any(e => e.Id == id);
        }
    }
}
