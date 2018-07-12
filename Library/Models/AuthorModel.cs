using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }

        [Required, Range(1, 100)]
        public string FullName { get; set; }
    }
}