using GastosControl.Application.Interfaces;
using GastosControl.Domain.Entities;
using GastosControl.Helpers;
using GastosControl.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GastosControl.Controllers
{
    [AuthorizeSession]
    public class DepositController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDepositService _depositService;

        public DepositController(ApplicationDbContext context, IDepositService depositService)
        {
            _context = context;
            _depositService = depositService;
        }

        // GET: Deposit
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var deposits = await _context.Deposits
                .Include(d => d.MonetaryFund)
                .Where(d => d.UserId == userId)
                .ToListAsync();

            return View(deposits);
        }


        // GET: Deposit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _context.Deposits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        // GET: Deposit/Create
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            ViewBag.MonetaryFundId = new SelectList(
                _context.MonetaryFunds.Where(f => f.UserId == userId),
                "Id", "Name"
            );
            return View();
        }

        // POST: Deposit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MonetaryFundId,Amount")] Deposit deposit)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Auth");

            if (ModelState.IsValid)
            {
                deposit.UserId = userId.Value;
                deposit.Date = DateTime.Now;

                await _depositService.AddAsync(deposit);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MonetaryFundId = new SelectList(
                _context.MonetaryFunds.Where(f => f.UserId == userId), "Id", "Name", deposit.MonetaryFundId
            );
            foreach (var modelState in ViewData.ModelState)
            {
                foreach (var error in modelState.Value.Errors)
                {
                    Console.WriteLine($"Error en '{modelState.Key}': {error.ErrorMessage}");
                }
            }

            return View(deposit);
        }

        // GET: Deposit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _context.Deposits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        // POST: Deposit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deposit = await _context.Deposits.FindAsync(id);
            if (deposit != null)
            {
                _context.Deposits.Remove(deposit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepositExists(int id)
        {
            return _context.Deposits.Any(e => e.Id == id);
        }
    }
}
