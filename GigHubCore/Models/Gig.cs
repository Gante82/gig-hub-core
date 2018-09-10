using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHubCore.Models
{
    public class Gig
    {
        public ICollection<Attendance> Attendances { get; private set; }

        public int Id { get; private set; }

        public bool IsCanceled { get; private set; }

        [Required]
        public string ArtistId { get; set; }

        public ApplicationUser Artist { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public Genre Genre { get; set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        internal void Update(DateTime dateTime, byte genereId, string venue)
        {
            var notification = Notification.GigUpdated(this, this.DateTime, this.Venue);

            this.DateTime = dateTime;
            this.GenreId = genereId;
            this.Venue = venue;

            foreach (var attendee in this.Attendances.Select(a => a.Attendee))
            {
                attendee.Notifty(notification);
            }
        }

        internal void Cancel()
        {
            this.IsCanceled = true;

            var notification = Notification.GigCancelled(this);

            foreach (var attendee in this.Attendances.Select(a => a.Attendee))
            {
                attendee.Notifty(notification);
            }
        }
    }
}
