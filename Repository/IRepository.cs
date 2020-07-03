using System;
using System.Linq;

namespace Repository
{
    public interface IRepository
    {
        void Add(Book book);
        void Add(Author author);
        void SaveChanges();

        IQueryable<Author> Authors { get; }
        IQueryable<Book> Books { get; }
    }
}
