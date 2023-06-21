using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Infrastructure.Validation;

namespace Trinity.Mvc.Domain
{
    public class MediaFile : Entity
    {
        public long Id { get; set; }

        public string File { get; set; } = "noimage.png";

        [NotMapped]
        [FileExtension]
        public IFormFile? FileUpload { get; set; }

        public long AlbumId { get; set; }
        public virtual Album? Album { get; set; }
    }
}