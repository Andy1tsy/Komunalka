using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Komunalka.DAL.KomunalDbContext;
using Komunalka.DAL.Models;

namespace Komunalka.MVC.Controllers
{
    public class PayingComponentsMVCController : Controller
    {
        private readonly KomunalContext _context;

        public PayingComponentsMVCController(KomunalContext context)
        {
            _context = context;
        }

        // GET: PayingComponentsMVC
        public async Task<IActionResult> Index()
        {
            var komunalContext = _context.PayingComponent.Include(p => p.Payment).Include(p => p.ServiceProvider);
            return View(await komunalContext.ToListAsync());
        }

        // GET: PayingComponentsMVC/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PayingComponent = await _context.PayingComponent
                .Include(p => p.Payment)
                .Include(p => p.ServiceProvider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (PayingComponent == null)
            {
                return NotFound();
            }

            return View(PayingComponent);
        }

        // GET: PayingComponentsMVC/Create
        public IActionResult Create()
        {
            ViewData["PaymentId"] = new SelectList(_context.Payment, "Id", "Id");
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProvider, "Id", "Name");
            return View();
        }

        // POST: PayingComponentsMVC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PaymentId,ServiceProviderId,Account,CounterIndicationsCurrent,CurrentIndicationsPrevious,CounterIndicationsDifference,RateCommon,RateDiscount,DiscountIndicationsAmount,Summa")] PayingComponent PayingComponent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(PayingComponent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentId"] = new SelectList(_context.Payment, "Id", "Id", PayingComponent.PaymentId);
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProvider, "Id", "Name", PayingComponent.ServiceProviderId);
            return View(PayingComponent);
        }

        // GET: PayingComponentsMVC/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PayingComponent = await _context.PayingComponent.FindAsync(id);
            if (PayingComponent == null)
            {
                return NotFound();
            }
            ViewData["PaymentId"] = new SelectList(_context.Payment, "Id", "Id", PayingComponent.PaymentId);
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProvider, "Id", "Name", PayingComponent.ServiceProviderId);
            return View(PayingComponent);
        }

        // POST: PayingComponentsMVC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PaymentId,ServiceProviderId,Account,CounterIndicationsCurrent,CurrentIndicationsPrevious,CounterIndicationsDifference,RateCommon,RateDiscount,DiscountIndicationsAmount,Summa")] PayingComponent PayingComponent)
        {
            if (id != PayingComponent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(PayingComponent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayingComponentExists(PayingComponent.Id))
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
            ViewData["PaymentId"] = new SelectList(_context.Payment, "Id", "Id", PayingComponent.PaymentId);
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProvider, "Id", "Name", PayingComponent.ServiceProviderId);
            return View(PayingComponent);
        }

        // GET: PayingComponentsMVC/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PayingComponent = await _context.PayingComponent
                .Include(p => p.Payment)
                .Include(p => p.ServiceProvider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (PayingComponent == null)
            {
                return NotFound();
            }

            return View(PayingComponent);
        }

        // POST: PayingComponentsMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var PayingComponent = await _context.PayingComponent.FindAsync(id);
            _context.PayingComponent.Remove(PayingComponent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayingComponentExists(int id)
        {
            return _context.PayingComponent.Any(e => e.Id == id);
        }
    }
}
