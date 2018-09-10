using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigHubCore.Models
{
    public class FollowUp
    {
        public ApplicationUser Artist { get; set; }

        public ApplicationUser Follower { get; set; }

        [Column(Order = 1)]
        [StringLength(450)]
        public string ArtistId { get; set; }

        [Column(Order = 2)]
        [StringLength(450)]
        public string FollowerId { get; set; }
    }
}
