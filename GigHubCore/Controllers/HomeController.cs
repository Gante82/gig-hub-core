using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GigHubCore.Models;
using GigHubCore.Data;
using Microsoft.EntityFrameworkCore;
using GigHubCore.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace GigHubCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var upcomingGigs = context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            var gigsViewModel = new GigsViewModel()
            {
                Heading = "Upcoming Gigs",
                ShowActions = this.User.Identity.IsAuthenticated,
                UpcomingGigs = upcomingGigs
            };

            return View("Gigs", gigsViewModel);

        }

        [HttpGet]
        public async Task<IActionResult> Following()
        {
            var user = await userManager.GetUserAsync(User);

            var followees = context.FollowUps
                .Where(f => f.FollowerId == user.Id)
                .Include(f => f.Artist)
                .Select(f => f.Artist);

            return View(followees);
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
