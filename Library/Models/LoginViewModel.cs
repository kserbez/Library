using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required"), Range(4, 254), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}