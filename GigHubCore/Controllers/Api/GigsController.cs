using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigHubCore.Data;
using GigHubCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GigHubCore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GigsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public GigsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Action()
        {
            return Ok();
        }


        [HttpDelete("{id:int}")]
        public IActionResult Cancel(int id)
        {
            var userId = userManager.GetUserId(User);

            var gig = context.Gigs
                .Include(g => g.Attendances)
                .ThenInclude(a => a.Attendee)
                .Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCanceled)
            {
                return NotFound();
            }

            gig.Cancel();

            context.SaveChanges();

            return Ok();
        }

    }
}