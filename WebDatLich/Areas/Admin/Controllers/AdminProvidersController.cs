using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDatLich.Models;

namespace WebDatLich.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProvidersController : Controller
    {
        private readonly AppointmentschedulerContext _context;

        public AdminProvidersController(AppointmentschedulerContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminProviders
        public async Task<IActionResult> Index()
        {
            var appointmentschedulerContext = _context.Providers.Include(p => p.IdProviderNavigation);
            return View(await appointmentschedulerContext.ToListAsync());
        }

        // GET: Admin/AdminProviders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers
                .Include(p => p.IdProviderNavigation)
                .FirstOrDefaultAsync(m => m.IdProvider == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // GET: Admin/AdminProviders/Create
        public IActionResult Create()
        {
            ViewData["IdProvider"] = new SelectList(_context.Users, "Id", "Id");
            // Thêm SelectList Works
            /* ViewData["Works"] = new SelectList(_context.Works, "Id", "Name");*/
            return View();
        }

        // POST: Admin/AdminProviders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProvider")] Provider provider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProvider"] = new SelectList(_context.Users, "Id", "Id", provider.IdProvider);

            /*ViewData["Works"] = new SelectList(_context.Works, "Id", "Name", provider.Works);*/
            return View(provider);
        }

        // GET: Admin/AdminProviders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }
            ViewData["IdProvider"] = new SelectList(_context.Users, "Id", "Id", provider.IdProvider);
            
            return View(provider);
        }

        // POST: Admin/AdminProviders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProvider")] Provider provider)
        {
            if (id != provider.IdProvider)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProviderExists(provider.IdProvider))
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
            ViewData["IdProvider"] = new SelectList(_context.Users, "Id", "Id", provider.IdProvider);
            return View(provider);
        }

        // GET: Admin/AdminProviders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers
                .Include(p => p.IdProviderNavigation)
                .FirstOrDefaultAsync(m => m.IdProvider == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // POST: Admin/AdminProviders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var provider = await _context.Providers.FindAsync(id);
            if (provider != null)
            {
                _context.Providers.Remove(provider);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderExists(int id)
        {
            return _context.Providers.Any(e => e.IdProvider == id);
        }
    }
}
