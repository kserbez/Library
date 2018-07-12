using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required, Range(4, 254), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}