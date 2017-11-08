using HerfstVakantie.Controllers;
using HerfstVakantie.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerfstVakantie.Services
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book GetBookById(int id);
        List<Genre> GetAllGenres();
        Author GetAuthorById(int id);
        List<Author> GetAllAuthors();
        List<Book> GetAllBooksSortedByName(BookCriteria criteria);
        Genre GetGenreById(int id);
        void Save(Book book);
        void DeleteFromId(int id);
    }
}
