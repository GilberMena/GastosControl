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
    public class MonetaryFundController : Controller
    {
        private readonly IMonetaryFundService _repository;
        private readonly ApplicationDbContext _context;

        public MonetaryFundController(IMonetaryFundService repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        // GET: MonetaryFund
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");
            return View(await _repository.GetByUserIdAsync((int)userId));
        }

        // GET: MonetaryFund/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monetaryFund = await _context.MonetaryFunds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monetaryFund == null)
            {
                return NotFound();
            }

            return View(monetaryFund);
        }

        // GET: MonetaryFund/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MonetaryFund/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] MonetaryFund monetaryFund)
        {
            monetaryFund.UserId = (int)HttpContext.Session.GetInt32("UserId")!;
            if (ModelState.IsValid)
            {
                _context.Add(monetaryFund);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monetaryFund);
        }

        // GET: MonetaryFund/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monetaryFund = await _context.MonetaryFunds.FindAsync(id);
            if (monetaryFund == null)
            {
                return NotFound();
            }
            return View(monetaryFund);
        }

        // POST: MonetaryFund/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] MonetaryFund monetaryFund)
        {
            if (id != monetaryFund.Id)
            {
                return NotFound();
            }
            monetaryFund.UserId = (int)HttpContext.Session.GetInt32("UserId")!;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monetaryFund);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonetaryFundExists(monetaryFund.Id))
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
            return View(monetaryFund);
        }

        // GET: MonetaryFund/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monetaryFund = await _context.MonetaryFunds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monetaryFund == null)
            {
                return NotFound();
            }

            return View(monetaryFund);
        }

        // POST: MonetaryFund/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monetaryFund = await _context.MonetaryFunds.FindAsync(id);
            if (monetaryFund != null)
            {
                _context.MonetaryFunds.Remove(monetaryFund);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonetaryFundExists(int id)
        {
            return _context.MonetaryFunds.Any(e => e.Id == id);
        }
    }
}
