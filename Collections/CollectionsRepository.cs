using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Collections
{
    public class CollectionsRepository : IRepository
    {
        private List<Book> _books = new List<Book>();
        private List<Author> _authors = new List<Author>();

        public IQueryable<Author> Authors => _authors.AsQueryable();
        public IQueryable<Book> Books => _books.AsQueryable();

        public void Add(Book book) => _books.Add(book);
        public void Add(Author author) => _authors.Add(author);

        public void SaveChanges()
        {
        }
    }
}
