using GigHubCore.Data;
using GigHubCore.Models;
using GigHubCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GigHubCore.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public GigsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Mine()
        {
            var userId = userManager.GetUserId(User);
            var myGigs = context.Gigs
                .Where(g =>
                g.ArtistId == userId &&
                g.DateTime > DateTime.Now &&
                !g.IsCanceled)
                .Include(g => g.Genre);

            return View(myGigs);
        }


        [Authorize]
        [HttpGet]
        public IActionResult Attending()
        {
            var userId = this.userManager.GetUserId(User);
            var gigs = this.context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Where(g => g.DateTime > DateTime.Now)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var gigsViewModel = new GigsViewModel()
            {
                Heading = "Gigs I'm Attending",
                ShowActions = this.User.Identity.IsAuthenticated,
                UpcomingGigs = gigs
            };

            return View("Gigs", gigsViewModel);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = userManager.GetUserId(User);
            var gig = context.Gigs
                .Single(g => g.Id == id && g.ArtistId == userId);

            var gigFormViewModel = new GigFormViewModel()
            {
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Genre = gig.GenreId,
                Genres = context.Genres,
                Time = gig.DateTime.ToString("HH:mm"),
                Venue = gig.Venue,
                Heading = "Edit a Gig",
                Id = gig.Id
            };

            return View("GigForm", gigFormViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(GigFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = userManager.GetUserId(User);
                Gig gig = context.Gigs
                    .Include(g => g.Attendances)
                    .ThenInclude(a => a.Attendee)
                    .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);
                gig.Update(viewModel.GetDateTime(), viewModel.Genre, viewModel.Venue);

                context.SaveChanges();

                return RedirectToAction("Mine", "Gigs");
            }

            viewModel.Genres = context.Genres;
            return View("GigForm", viewModel);
        }


        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            var gigFormViewModel = new GigFormViewModel()
            {
                Genres = context.Genres,
                Heading = "Add a Gig"
            };

            return View("GigForm", gigFormViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GigFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Gig gig = new Gig()
                {
                    ArtistId = userManager.GetUserId(User),
                    DateTime = viewModel.GetDateTime(),
                    GenreId = viewModel.Genre,
                    Venue = viewModel.Venue
                };

                context.Gigs.Add(gig);
                context.SaveChanges();

                return RedirectToAction("Mine", "Gigs");
            }

            viewModel.Genres = context.Genres;
            return View("GigForm", viewModel);
        }
    }
}
