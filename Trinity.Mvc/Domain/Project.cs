using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Infrastructure.Validation;

namespace Trinity.Mvc.Domain
{
    public class Project : Entity
    {
        public Project() {}
        
        public long Id { get; set; }
        
        public string Slug { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Photo { get; set; } = "noimage.png";

        [NotMapped]
        [FileExtension]
        public IFormFile? PhotoUpload { get; set; }

        public bool Closed { get; set; } = false;

        public bool Published { get; set; } = false;

        public long? CauseId { get; set; }
        public virtual Cause? Cause { get; set; }

        public string ManagerId { get; set; } = string.Empty;
        public virtual ApplicationUser? Manager { get; set; }

        public virtual Proposal? Proposal { get; set; }

        public virtual Album? Album { get; set; }

        public virtual Fundraiser? Fundraiser { get; set; }

        public virtual ICollection<Expenditure> Expenditures { get; set; } = new List<Expenditure>();
        public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
        public virtual ICollection<ProjectSupporter> Supporters { get; set; } = new List<ProjectSupporter>();
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
        public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}