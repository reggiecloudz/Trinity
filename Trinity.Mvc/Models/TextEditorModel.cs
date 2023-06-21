using System.ComponentModel.DataAnnotations;

namespace Trinity.Mvc.Models
{
    public class TextEditorModel
    {
        [DataType(DataType.Text)]
        public string Content { get; set; } = string.Empty;
    }
}
