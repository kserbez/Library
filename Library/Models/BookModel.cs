using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Quantity { get; set; }


        public List<AuthorModel> Authors;
    }
}