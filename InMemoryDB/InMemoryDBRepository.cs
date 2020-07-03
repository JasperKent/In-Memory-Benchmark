using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Linq;

namespace InMemoryDB
{
    public class InMemoryDBRepository : IRepository
    {
        private readonly LibraryContext _context;

        public InMemoryDBRepository()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                            .UseInMemoryDatabase(databaseName: "MockDB")
                            .Options;

            _context = new LibraryContext(options);
        }

        public IQueryable<Author> Authors => _context.Authors;
        public IQueryable<Book> Books => _context.Books;

        public void Add(Book book) => _context.Add(book);
        public void Add(Author author) => _context.Add(author);

        public Book GetBySQL()
        {
            return _context.Books.FromSqlRaw("SELECT * FROM Books WHERE Title='Book_20'").Single();
        }

        public void SaveChanges() => _context.SaveChanges();
    }
}
