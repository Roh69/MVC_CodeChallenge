using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_CodeChallenge.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_CodeChallenge.Models
{
    public class BookModel
    {
  
        public int bookId{ get; set; }
        public string title { get; set; }
        public string genre { get; set; }
        public decimal price { get; set; }
        public int authorId { get; set; }

        public AuthorModel author { get; set; }
    }
}