using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GigHubCore.Data;
using GigHubCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using GigHubCore.Views.Dtos;

namespace GigHubCore.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public AttendancesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        public IActionResult Attend(AttendanceDto dto)
        {
            string userId = userManager.GetUserId(User);

            if (context.Attendances.Any(a => a.GigId == dto.GigId && a.AttendeeId == userId))
            {
                return BadRequest("The attendance already exists.");
            }

            var attendance = new Attendance()
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            context.Attendances.Add(attendance);
            context.SaveChanges();

            return Ok();
        }

        // GET: api/Attendances
        [HttpGet]
        public IEnumerable<Attendance> GetAttendances()
        {
            return context.Attendances;
        }

        // GET: api/Attendances/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttendance([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attendance = await context.Attendances.FindAsync(id);

            if (attendance == null)
            {
                return NotFound();
            }

            return Ok(attendance);
        }

        // PUT: api/Attendances/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendance([FromRoute] int id, [FromBody] Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != attendance.GigId)
            {
                return BadRequest();
            }

            context.Entry(attendance).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Attendances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attendance = await context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            context.Attendances.Remove(attendance);
            await context.SaveChangesAsync();

            return Ok(attendance);
        }

        private bool AttendanceExists(int id)
        {
            return context.Attendances.Any(e => e.GigId == id);
        }
    }
}