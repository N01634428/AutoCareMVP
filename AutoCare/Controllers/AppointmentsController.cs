using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoCare.API.Data; // Adjust based on your namespace
using AutoCare.API.Models; // Adjust based on your namespace
using AutoCare.API.DTOs;
using AutoCare.Models;

namespace AutoCare.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly AutoCareContext _context;

        public AppointmentsController(AutoCareContext context)
        {
            _context = context;
        }

        // GET: api/appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }

        // GET: api/appointments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
                return NotFound();

            return appointment;
        }

        // POST: api/appointments
        [HttpPost]
        public async Task<ActionResult<Appointment>> CreateAppointment(AppointmentDTO dto)
        {
            var appointment = new Appointment
            {
                CustomerName = dto.CustomerName,
                Phone = dto.Phone,
                Email = dto.Email,
                VehicleMake = dto.VehicleMake,
                VehicleModel = dto.VehicleModel,
                VehicleYear = dto.VehicleYear,
                ServiceName = dto.ServiceName,
                PreferredDateTime = dto.PreferredDateTime
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        // PUT: api/appointments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, AppointmentDTO dto)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            appointment.CustomerName = dto.CustomerName;
            appointment.Phone = dto.Phone;
            appointment.Email = dto.Email;
            appointment.VehicleMake = dto.VehicleMake;
            appointment.VehicleModel = dto.VehicleModel;
            appointment.VehicleYear = dto.VehicleYear;
            appointment.ServiceName = dto.ServiceName;
            appointment.PreferredDateTime = dto.PreferredDateTime;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/appointments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
