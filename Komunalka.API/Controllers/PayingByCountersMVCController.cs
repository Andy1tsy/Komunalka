using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Komunalka.DAL.KomunalDbContext;
using Komunalka.DAL.Models;

namespace Komunalka.API.Controllers
{
    public class PayingByCountersMVCController : Controller
    {
        private readonly KomunalContext _context;

        public PayingByCountersMVCController(KomunalContext context)
        {
            _context = context;
        }

        // GET: PayingByCountersMVC
        public async Task<IActionResult> Index()
        {
            var komunalContext = _context.PayingByCounter.Include(p => p.Payment).Include(p => p.ServiceProvider);
            return View(await komunalContext.ToListAsync());
        }

        // GET: PayingByCountersMVC/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payingByCounter = await _context.PayingByCounter
                .Include(p => p.Payment)
                .Include(p => p.ServiceProvider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payingByCounter == null)
            {
                return NotFound();
            }

            return View(payingByCounter);
        }

        // GET: PayingByCountersMVC/Create
        public IActionResult Create()
        {
            ViewData["PaymentId"] = new SelectList(_context.Payment, "Id", "Id");
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProvider, "Id", "Name");
            return View();
        }

        // POST: PayingByCountersMVC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PaymentId,ServiceProviderId,Account,CounterIndicationsCurrent,CurrentIndicationsPrevious,CounterIndicationsDifference,RateCommon,RateDiscount,DiscountIndicationsAmount,Summa")] PayingByCounter payingByCounter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payingByCounter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentId"] = new SelectList(_context.Payment, "Id", "Id", payingByCounter.PaymentId);
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProvider, "Id", "Name", payingByCounter.ServiceProviderId);
            return View(payingByCounter);
        }

        // GET: PayingByCountersMVC/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payingByCounter = await _context.PayingByCounter.FindAsync(id);
            if (payingByCounter == null)
            {
                return NotFound();
            }
            ViewData["PaymentId"] = new SelectList(_context.Payment, "Id", "Id", payingByCounter.PaymentId);
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProvider, "Id", "Name", payingByCounter.ServiceProviderId);
            return View(payingByCounter);
        }

        // POST: PayingByCountersMVC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PaymentId,ServiceProviderId,Account,CounterIndicationsCurrent,CurrentIndicationsPrevious,CounterIndicationsDifference,RateCommon,RateDiscount,DiscountIndicationsAmount,Summa")] PayingByCounter payingByCounter)
        {
            if (id != payingByCounter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payingByCounter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayingByCounterExists(payingByCounter.Id))
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
            ViewData["PaymentId"] = new SelectList(_context.Payment, "Id", "Id", payingByCounter.PaymentId);
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProvider, "Id", "Name", payingByCounter.ServiceProviderId);
            return View(payingByCounter);
        }

        // GET: PayingByCountersMVC/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payingByCounter = await _context.PayingByCounter
                .Include(p => p.Payment)
                .Include(p => p.ServiceProvider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payingByCounter == null)
            {
                return NotFound();
            }

            return View(payingByCounter);
        }

        // POST: PayingByCountersMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payingByCounter = await _context.PayingByCounter.FindAsync(id);
            _context.PayingByCounter.Remove(payingByCounter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayingByCounterExists(int id)
        {
            return _context.PayingByCounter.Any(e => e.Id == id);
        }
    }
}
