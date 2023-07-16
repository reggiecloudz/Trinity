using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Models
{
    public class SceneResponseModel
    {
        public long Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
        
        public string ScenePhoto { get; set; } = string.Empty;

        public string ProjectName { get; set; } = string.Empty;

        public string ProjectPhoto { get; set; } = string.Empty;

        public string ProjectManager { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

    }
}