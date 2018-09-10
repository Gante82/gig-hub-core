using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GigHubCore.Data;
using GigHubCore.Models;
using GigHubCore.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GigHubCore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowUpsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public FollowUpsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        public IActionResult Follow(FollowUpDto dto)
        {
            string userId = userManager.GetUserId(User);

            if (context.FollowUps.Any(f => f.ArtistId == dto.ArtistId && f.FollowerId == userId))
            {
                return BadRequest("The relationship already exists.");
            }

            var followUp = new FollowUp()
            {
                ArtistId = dto.ArtistId,
                FollowerId = userId
            };

            context.FollowUps.Add(followUp);
            context.SaveChanges();

            return Ok();
        }

        // GET: api/FollowUps
        [HttpGet]
        public IEnumerable<FollowUp> GetFollowUp()
        {
            return context.FollowUp;
        }

        // GET: api/FollowUps/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFollowUp([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followUp = await context.FollowUp.FindAsync(id);

            if (followUp == null)
            {
                return NotFound();
            }

            return Ok(followUp);
        }

        // PUT: api/FollowUps/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollowUp([FromRoute] string id, [FromBody] FollowUp followUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != followUp.ArtistId)
            {
                return BadRequest();
            }

            context.Entry(followUp).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowUpExists(id))
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

        // POST: api/FollowUps
        //[HttpPost]
        //public async Task<IActionResult> PostFollowUp([FromBody] FollowUp followUp)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.FollowUp.Add(followUp);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (FollowUpExists(followUp.ArtistId))
        //        {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetFollowUp", new { id = followUp.ArtistId }, followUp);
        //}

        // DELETE: api/FollowUps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFollowUp([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followUp = await context.FollowUp.FindAsync(id);
            if (followUp == null)
            {
                return NotFound();
            }

            context.FollowUp.Remove(followUp);
            await context.SaveChangesAsync();

            return Ok(followUp);
        }

        private bool FollowUpExists(string id)
        {
            return context.FollowUp.Any(e => e.ArtistId == id);
        }
    }
}