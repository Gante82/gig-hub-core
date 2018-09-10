using System;
using System.Collections.Generic;
using System.Text;
using GigHubCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigHubCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Gig> Gigs { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<FollowUp> FollowUps { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserNotification>()
                .HasKey(u => new { u.UserId, u.NotificationId });

            builder.Entity<UserNotification>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserNotifications)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Attendance>()
                .HasKey(a => new { a.GigId, a.AttendeeId });

            builder.Entity<Attendance>()
                .HasOne(a => a.Gig)
                .WithMany(g => g.Attendances)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<FollowUp>()
                .HasKey(f => new { f.ArtistId, f.FollowerId });

            builder.Entity<FollowUp>()
                .HasOne(f => f.Artist)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Artists)
                .WithOne(a => a.Follower)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)
                .WithOne(f => f.Artist)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        public DbSet<GigHubCore.Models.FollowUp> FollowUp { get; set; }
    }
}
