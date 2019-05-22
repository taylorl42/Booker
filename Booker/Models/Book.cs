using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booker.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string BookName { get; set; }

        public string BookAuthor { get; set; }

        public int YearOfPublication { get; set; }

        public int NumberInStock { get; set; }

        public int NumberRented { get; set; }
    }
}