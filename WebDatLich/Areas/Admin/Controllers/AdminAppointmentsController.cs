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
    public class AdminAppointmentsController : Controller
    {
        private readonly AppointmentschedulerContext _context;

        public AdminAppointmentsController(AppointmentschedulerContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminAppointments
        public async Task<IActionResult> Index()
        {
            var appointmentschedulerContext = _context.Appointments.Include(a => a.IdCancelerNavigation).Include(a => a.IdCustomerNavigation).Include(a => a.IdInvoiceNavigation).Include(a => a.IdProviderNavigation).Include(a => a.IdWorkNavigation);
            return View(await appointmentschedulerContext.ToListAsync());
        }

        // GET: Admin/AdminAppointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.IdCancelerNavigation)
                .Include(a => a.IdCustomerNavigation)
                .Include(a => a.IdInvoiceNavigation)
                .Include(a => a.IdProviderNavigation)
                .Include(a => a.IdWorkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Admin/AdminAppointments/Create
        public IActionResult Create()
        {
            ViewData["IdCanceler"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["IdCustomer"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["IdInvoice"] = new SelectList(_context.Invoices, "Id", "Id");
            ViewData["IdProvider"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["IdWork"] = new SelectList(_context.Works, "Id", "Id");
            return View();
        }

        // POST: Admin/AdminAppointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Start,End,CanceledAt,Status,IdCanceler,IdProvider,IdCustomer,IdWork,IdInvoice")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCanceler"] = new SelectList(_context.Users, "Id", "Id", appointment.IdCanceler);
            ViewData["IdCustomer"] = new SelectList(_context.Users, "Id", "Id", appointment.IdCustomer);
            ViewData["IdInvoice"] = new SelectList(_context.Invoices, "Id", "Id", appointment.IdInvoice);
            ViewData["IdProvider"] = new SelectList(_context.Users, "Id", "Id", appointment.IdProvider);
            ViewData["IdWork"] = new SelectList(_context.Works, "Id", "Id", appointment.IdWork);
            return View(appointment);
        }

        // GET: Admin/AdminAppointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["IdCanceler"] = new SelectList(_context.Users, "Id", "Id", appointment.IdCanceler);
            ViewData["IdCustomer"] = new SelectList(_context.Users, "Id", "Id", appointment.IdCustomer);
            ViewData["IdInvoice"] = new SelectList(_context.Invoices, "Id", "Id", appointment.IdInvoice);
            ViewData["IdProvider"] = new SelectList(_context.Users, "Id", "Id", appointment.IdProvider);
            ViewData["IdWork"] = new SelectList(_context.Works, "Id", "Id", appointment.IdWork);
            return View(appointment);
        }

        // POST: Admin/AdminAppointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Start,End,CanceledAt,Status,IdCanceler,IdProvider,IdCustomer,IdWork,IdInvoice")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
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
            ViewData["IdCanceler"] = new SelectList(_context.Users, "Id", "Id", appointment.IdCanceler);
            ViewData["IdCustomer"] = new SelectList(_context.Users, "Id", "Id", appointment.IdCustomer);
            ViewData["IdInvoice"] = new SelectList(_context.Invoices, "Id", "Id", appointment.IdInvoice);
            ViewData["IdProvider"] = new SelectList(_context.Users, "Id", "Id", appointment.IdProvider);
            ViewData["IdWork"] = new SelectList(_context.Works, "Id", "Id", appointment.IdWork);
            return View(appointment);
        }

        // GET: Admin/AdminAppointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.IdCancelerNavigation)
                .Include(a => a.IdCustomerNavigation)
                .Include(a => a.IdInvoiceNavigation)
                .Include(a => a.IdProviderNavigation)
                .Include(a => a.IdWorkNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Admin/AdminAppointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
    }
}
