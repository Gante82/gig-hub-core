using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigHubCore.Models
{
    public class UserNotification
    {
        [Column(Order = 1)]
        public string UserId { get; set; }

        [Column(Order = 2)]
        public int NotificationId { get; set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public bool IsRead { get; set; }

        protected UserNotification()
        {

        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (notification == null)
            {
                throw new ArgumentNullException("notification");
            }

            this.User = user;
            this.Notification = notification;
        }
    }
}
