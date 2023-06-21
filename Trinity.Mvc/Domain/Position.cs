using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Position : Entity
    {
        public Position() {}
        
        public long Id { get; set; }

        public string Slug { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Details { get; set; } = string.Empty;

        public bool Closed { get; set; } = false;

        public uint PeopleNeeded { get; set; } = 1;

        public uint PositionsFilled { get; set; } = 0;

        public long ProjectId { get; set; }
        public Project? Project { get; set; }

        public ICollection<JobRequirement> JobRequirements { get; set; } = new List<JobRequirement>();
        public ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();
    }
}