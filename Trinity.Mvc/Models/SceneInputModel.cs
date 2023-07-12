using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Infrastructure.Validation;

namespace Trinity.Mvc.Models
{
    public class SceneInputModel
    {
        public SceneInputModel() {}
        
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
        
        [NotMapped]
        [FileExtension]
        public IFormFile? PhotoUpload { get; set; }

        public long JourneyId { get; set; }

        public long ProjectId { get; set; }
    }
}