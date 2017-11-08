using HerfstVakantie.Controllers;
using HerfstVakantie.Data;
using HerfstVakantie.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerfstVakantie.Services
{
    public class BookService : IBookService
    {
        private readonly EntityContext _entityContext;

        public BookService(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        private IIncludableQueryable<Book, Author> GetFullGraph()
        {
            return _entityContext.Books.Include(x => x.Genre).Include(x => x.Authors).ThenInclude(x => x.Author);
        }

        public List<Book> GetAllBooks()
        {
            return GetFullGraph().OrderBy(x => x.Title)
                .ToList();
        }

        public Book GetBookById(int id)
        {
            return GetFullGraph()
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Genre> GetAllGenres()
        {
            return _entityContext.Genre.ToList();
        }

        public Genre GetGenreById(int id)
        {
            return _entityContext.Genre.Find(id);
        }

        public List<Author> GetAllAuthors()
        {
            return _entityContext.Authors.ToList();
        }

        public Author GetAuthorById(int id)
        {
            return _entityContext.Authors.Find(id);
        }

        public void Save(Book book)
        {
            if (book.Id == 0)
                _entityContext.Books.Add(book);
            else
                _entityContext.Books.Update(book);
            _entityContext.SaveChanges();
        }

        public void DeleteFromId(int id)
        {
            var toDelete = GetBookById(id);
            if (toDelete != null)
            {
                _entityContext.Books.Remove(toDelete);
                _entityContext.SaveChanges();
            }
        }

        public List<Book> GetAllBooksSortedByName(BookCriteria criteria)
        {
            return GetFullGraph()
                .Where(x => string.IsNullOrEmpty(criteria.Title) ||
                            string.Equals(x.Title, criteria.Title, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(x => x.Title).ToList();
        }
    }
}
