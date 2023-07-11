using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Infrastructure.Validation;

namespace Trinity.Mvc.Domain
{
    public class Scene : Entity
    {
        public Scene() {}

        public long Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        // public int SceneNumber { get; set; }

        public string Photo { get; set; } = "noimage.png";

        [NotMapped]
        [FileExtension]
        public IFormFile? PhotoUpload { get; set; }

        public long JourneyId { get; set; }
        public virtual Journey? Journey { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}