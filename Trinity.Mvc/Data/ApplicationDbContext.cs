#nullable disable
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Cause> Causes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectSupporter> ProjectSupporters { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Position> Positions { get; set; } 
        public DbSet<JobRequirement> JobRequirements { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DiscussionGroup> DiscussionGroups { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Expenditure> Expenditures { get; set; } 
        public DbSet<Album> Albums { get; set; }
        public DbSet<MediaFile> MediaFiles { get; set; }
        public DbSet<Fundraiser> Fundraisers { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<EventAttendee> EventAttendees { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<ConnectionRequest> ConnectionRequests { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserNotification>(notif =>
            {
                notif.HasKey(un => new { un.UserId, un.NotificationId });

                notif.HasOne(un => un.User)
                    .WithMany(m => m.Notifications)
                    .HasForeignKey(un => un.UserId)
                    .IsRequired();

                notif.HasOne(un => un.Notification)
                    .WithMany(n => n.Users)
                    .HasForeignKey(un => un.NotificationId)
                    .IsRequired();
            });

            builder.Entity<Subscription>(obj =>
            {
                obj.HasKey(un => new { un.DiscussionGroupId, un.UserId });

                obj.HasOne(un => un.DiscussionGroup)
                    .WithMany(m => m.Subscribers)
                    .HasForeignKey(un => un.DiscussionGroupId)
                    .IsRequired();

                obj.HasOne(un => un.User)
                    .WithMany(n => n.Subscriptions)
                    .HasForeignKey(un => un.UserId)
                    .IsRequired();
            });

            builder.Entity<EventAttendee>(ea =>
            {
                ea.HasKey(un => new { un.EventId, un.AttendeeId });

                ea.HasOne(un => un.Event)
                    .WithMany(m => m.Attendees)
                    .HasForeignKey(un => un.EventId)
                    .IsRequired();

                ea.HasOne(un => un.Attendee)
                    .WithMany(n => n.Events)
                    .HasForeignKey(un => un.AttendeeId)
                    .IsRequired();
            });

            builder.Entity<ProjectSupporter>(ps =>
            {
                ps.HasKey(un => new { un.ProjectId, un.SupporterId });

                ps.HasOne(un => un.Project)
                    .WithMany(m => m.Supporters)
                    .HasForeignKey(un => un.ProjectId)
                    .IsRequired();

                ps.HasOne(un => un.Supporter)
                    .WithMany(n => n.ProjectsSupported)
                    .HasForeignKey(un => un.SupporterId)
                    .IsRequired();
            });

            builder.Entity<Like>(obj =>
            {
                obj.HasKey(un => new { un.PostId, un.UserId });

                obj.HasOne(un => un.Post)
                    .WithMany(m => m.Likes)
                    .HasForeignKey(un => un.PostId)
                    .IsRequired();

                obj.HasOne(un => un.User)
                    .WithMany(n => n.Likes)
                    .HasForeignKey(un => un.UserId)
                    .IsRequired();
            });

            builder.Entity<Vote>(obj =>
            {
                obj.HasKey(un => new { un.ReplyId, un.UserId });

                obj.HasOne(un => un.Reply)
                    .WithMany(m => m.Votes)
                    .HasForeignKey(un => un.ReplyId)
                    .IsRequired();

                obj.HasOne(un => un.User)
                    .WithMany(n => n.Votes)
                    .HasForeignKey(un => un.UserId)
                    .IsRequired();
            });
        }

        public override int SaveChanges()
        {
            var changedEntities = ChangeTracker.Entries();

            foreach (var changedEntity in changedEntities)
            {
                if (changedEntity.Entity is Entity)
                {
                    var entity = changedEntity.Entity as Entity;
                    if (changedEntity.State == EntityState.Added)
                    {
                        entity.Created = DateTime.Now;
                        entity.Updated = DateTime.Now;

                    }
                    else if (changedEntity.State == EntityState.Modified)
                    {
                        entity.Updated = DateTime.Now;
                    }
                }

            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var changedEntities = ChangeTracker.Entries();

            foreach (var changedEntity in changedEntities)
            {
                if (changedEntity.Entity is Entity)
                {
                    var entity = changedEntity.Entity as Entity;
                    if (changedEntity.State == EntityState.Added)
                    {
                        entity.Created = DateTime.Now;
                        entity.Updated = DateTime.Now;

                    }
                    else if (changedEntity.State == EntityState.Modified)
                    {
                        entity.Updated = DateTime.Now;
                    }
                }
            }
            return (await base.SaveChangesAsync(true, cancellationToken));
        }
    }

}