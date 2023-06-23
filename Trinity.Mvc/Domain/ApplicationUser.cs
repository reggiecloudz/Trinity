using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Trinity.Mvc.Infrastructure.Validation;

namespace Trinity.Mvc.Domain
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string FullName { get; set; } = string.Empty;

        public string AvatarImage { get; set; } = "noimage.png";

        public string CoverImage { get; set; } = "noimage.png";

        [NotMapped]
        [FileExtension]
        public IFormFile? AvatarImageUpload { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile? CoverImageUpload { get; set; }

        public string Occupation { get; set; } = "Not Provided";

        public virtual ICollection<DiscussionGroup> Discussions { get; set; } = new List<DiscussionGroup>();
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
        public virtual ICollection<ProjectSupporter> ProjectsSupported { get; set; } = new List<ProjectSupporter>();
        public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<Reply> Replies { get; set; } = new List<Reply>();
        public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
        public virtual ICollection<Applicant> Applications { get; set; } = new List<Applicant>();
        public virtual ICollection<EventAttendee> Events { get; set; } = new List<EventAttendee>();
        public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();
        public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public virtual ICollection<UserNotification> Notifications { get; set; } = new List<UserNotification>();
    }
}