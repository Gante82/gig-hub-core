using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigHubCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<FollowUp> Followers { get; set; }
        public ICollection<FollowUp> Artists { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }

        public ApplicationUser()
        {
            Followers = new Collection<FollowUp>();
            Artists = new Collection<FollowUp>();
            UserNotifications = new Collection<UserNotification>();
        }

        public void Notifty(Notification notification)
        {
            UserNotifications.Add(new UserNotification(this, notification));
        }
    }
}
