using GigHubCore.Controllers;
using GigHubCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace GigHubCore.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate(ErrorMessage = "The date selected is in the past.")]
        public string Date { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.Parse($"{Date} {Time}");
        }

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<GigsController, IActionResult>> update = c => c.Update(this);
                Expression<Func<GigsController, IActionResult>> create = c => c.Create(this);

                var action = Id == 0 ? create : update;

                return (action.Body as MethodCallExpression).Method.Name;
            }
        }
    }
}
