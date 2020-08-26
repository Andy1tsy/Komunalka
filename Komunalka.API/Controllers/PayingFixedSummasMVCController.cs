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
    public class PayingFixedSummasMVCController : Controller
    {
        private readonly KomunalContext _context;

        public PayingFixedSummasMVCController(KomunalContext context)
        {
            _context = context;
        }

        // GET: PayingFixedSummasMVC
        public async Task<IActionResult> Index()
        {
            var komunalContext = _context.PayingFixedSumma.Include(p => p.Payment).Include(p => p.ServiceProvider);
            return View(await komunalContext.ToListAsync());
        }

        // GET: PayingFixedSummasMVC/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payingFixedSumma = await _context.PayingFixedSumma
                .Include(p => p.Payment)
                .Include(p => p.ServiceProvider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payingFixedSumma == null)
            {
                return NotFound();
            }

            return View(payingFixedSumma);
        }

        // GET: PayingFixedSummasMVC/Create
        public IActionResult Create()
        {
            ViewData["PaymentId"] = new SelectList(_context.Payment, "Id", "Id");
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProvider, "Id", "Name");
            return View();
        }

        // POST: PayingFixedSummasMVC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PaymentId,ServiceProviderId,Account,Summa")] PayingFixedSumma payingFixedSumma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payingFixedSumma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentId"] = new SelectList(_context.Payment, "Id", "Id", payingFixedSumma.PaymentId);
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProvider, "Id", "Name", payingFixedSumma.ServiceProviderId);
            return View(payingFixedSumma);
        }

        // GET: PayingFixedSummasMVC/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payingFixedSumma = await _context.PayingFixedSumma.FindAsync(id);
            if (payingFixedSumma == null)
            {
                return NotFound();
            }
            ViewData["PaymentId"] = new SelectList(_context.Payment, "Id", "Id", payingFixedSumma.PaymentId);
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProvider, "Id", "Name", payingFixedSumma.ServiceProviderId);
            return View(payingFixedSumma);
        }

        // POST: PayingFixedSummasMVC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PaymentId,ServiceProviderId,Account,Summa")] PayingFixedSumma payingFixedSumma)
        {
            if (id != payingFixedSumma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payingFixedSumma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayingFixedSummaExists(payingFixedSumma.Id))
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
            ViewData["PaymentId"] = new SelectList(_context.Payment, "Id", "Id", payingFixedSumma.PaymentId);
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProvider, "Id", "Name", payingFixedSumma.ServiceProviderId);
            return View(payingFixedSumma);
        }

        // GET: PayingFixedSummasMVC/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payingFixedSumma = await _context.PayingFixedSumma
                .Include(p => p.Payment)
                .Include(p => p.ServiceProvider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payingFixedSumma == null)
            {
                return NotFound();
            }

            return View(payingFixedSumma);
        }

        // POST: PayingFixedSummasMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payingFixedSumma = await _context.PayingFixedSumma.FindAsync(id);
            _context.PayingFixedSumma.Remove(payingFixedSumma);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayingFixedSummaExists(int id)
        {
            return _context.PayingFixedSumma.Any(e => e.Id == id);
        }
    }
}
