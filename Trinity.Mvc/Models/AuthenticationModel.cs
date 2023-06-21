using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Models
{
    public class AuthenticationModel
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [DataType(DataType.Password), Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
        public string Password { get; set; } = string.Empty;
    }
}