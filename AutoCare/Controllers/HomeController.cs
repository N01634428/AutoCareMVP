using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoCare.API.Data;
using AutoCare.Models;

namespace AutoCare.Controllers
{
    public class HomeController : Controller
    {
        private readonly AutoCareContext _context;

        public HomeController(AutoCareContext context)
        {
            _context = context;
        }

        // HOME: Landing Page
        public IActionResult Index()
        {
            return View();
        }

        // READ: List all bookings
        public async Task<IActionResult> ManageBooking()
        {
            var appointments = await _context.Appointments.ToListAsync();
            return View(appointments);
        }

        // CREATE: Show booking form
        [HttpGet]
        public IActionResult BookAppointment()
        {
            return View();
        }

        // CREATE: Save booking form
        [HttpPost]
        public async Task<IActionResult> BookAppointment(Appointment model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.Appointments.Add(model);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Appointment booked successfully!";
            return RedirectToAction(nameof(ManageBooking));
        }

        // UPDATE: Show edit form
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        // UPDATE: Save edited form
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Appointment model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Appointment updated successfully!";
                return RedirectToAction(nameof(ManageBooking));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Appointments.Any(a => a.Id == id))
                    return NotFound();
                else
                    throw;
            }
        }

        // DELETE: Show delete confirmation
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        // DELETE: Confirm deletion
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Appointment deleted successfully!";
            return RedirectToAction(nameof(ManageBooking));
        }
    }
}
