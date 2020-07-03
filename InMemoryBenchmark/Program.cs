using Collections;
using InMemoryDB;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InMemoryBenchmark
{
    class Program
    {
        private static IRepository _repository;

        static void Main()
        {
            _repository = new CollectionsRepository();
            //_repository = new InMemoryDBRepository();

            // Populate(10, 5);

            Timestamp(() => Populate(1000, 1000),"Populate");
            Timestamp(() => CountByReference(), "CountByReference");
            Timestamp(() => CountByJoin(), "CountByJoin");
        }

        private static void Timestamp (Action action,string name)
        {
            DateTime start = DateTime.UtcNow;

            action();

            Console.WriteLine($"{name} took {(DateTime.UtcNow - start).TotalMilliseconds:N0}ms.");

        }

        private static void CountByReference()
        {
            // Author_999
            // 123456789A
            var authors = from a in _repository.Authors 
                          where a.Name.Length < 10 
                          select a;

            int stub = "Book_".Length;

            int bookCount = authors.SelectMany(a => a.Books).Count(b => int.Parse(b.Title.Substring(stub, b.Title.Length - stub)) % 3 == 0);

            Console.WriteLine($"{bookCount} books matched.");
        }

        private static void CountByJoin()
        {
            // Author_999
            // 123456789A
            var books = from a in _repository.Authors
                        where a.Name.Length < 10
                        join b in _repository.Books on a.Name equals b.AuthorName
                        select b;

            int stub = "Book_".Length;

            int bookCount = books.Count(b => int.Parse(b.Title.Substring(stub, b.Title.Length - stub)) % 3 == 0);

            Console.WriteLine($"{bookCount} books matched.");
        }

        private static void Populate(int authorTotal, int bookTotal)
        {
            int bookNum = 0;

            for (int aCount = 0; aCount < authorTotal; ++aCount)
            {
                Author author = new Author { Name = $"Author_{aCount}", Books = new List<Book>() };

                for (int bCount = 0; bCount < bookTotal; ++bCount)
                {
                    Book book = new Book { Title = $"Book_{++bookNum}", AuthorName = author.Name };

                    book.Author = author;

                    author.Books.Add(book);

                    _repository.Add(book);
                }

                _repository.Add(author);
            }

            _repository.SaveChanges();
        }
    }
}
