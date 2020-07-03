using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public Author Author { get; set; }
    }
}
