using HerfstVakantie.Entities;
using HerfstVakantie.Models;
using HerfstVakantie.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerfstVakantie.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("/books")]
        public IActionResult Index()
        {
            var model = new BookListViewModel { Books = new List<BookDetailViewModel>() };
            model.Books.AddRange(_bookService.GetAllBooks().Select(ConvertBookDetail).ToList());
            return View(model);
        }

        [HttpGet("/books/{id}")]
        public IActionResult Detail([FromRoute] int id)
        {
            BookEditViewModel model;
            var selectedBook = _bookService.GetBookById(id);
            if (selectedBook == null) {
                model = new BookEditViewModel
                {
                    Id = 0,
                    Title = "",
                    CreationDate = DateTime.Now,
                    Genre = "",
                    GenreId = 0
                };
            } else
            {
                model = ConvertBookEdit(selectedBook);
            }

            model.Genres = _bookService.GetAllGenres().Select(x => new SelectListItem {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            model.Authors = _bookService.GetAllAuthors().Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);
        }

        [HttpPost("/books/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _bookService.DeleteFromId(id);
            return Redirect("/books");
        }

        [HttpPost("/books")]
        public IActionResult Save([FromForm] BookEditViewModel form)
        {
            Book book;
            if (form.Id == 0)
            {
                book = new Book();
            } else
            {
                book = _bookService.GetBookById(form.Id);
            }

            book.Title = form.Title;

            if (form.GenreId.HasValue)
            {
                book.Genre = _bookService.GetGenreById((int) form.GenreId.Value);
            } else
            {
                book.Genre = null;
            }

            if (form.AuthorId1.HasValue)
            {
                book.Authors[0] = new BookAuthor() {
                    Author = _bookService.GetAuthorById((int)form.AuthorId1.Value)
                };
            }

            if (form.AuthorId2.HasValue)
            {
                book.Authors[1] = new BookAuthor()
                {
                    Author = _bookService.GetAuthorById((int)form.AuthorId2.Value)
                };
            }

            book.CreationDate = form.CreationDate;
            book.ISBN = form.ISBN;
            _bookService.Save(book);

            return Redirect("/books");
        }

        protected BookDetailViewModel ConvertBookDetail(Book book)
        {
            return new BookDetailViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                CreationDate = book.CreationDate,
                Author = string.Join(";", book.Authors.Select(x => x.Author.FullName)),
                Genre = book.Genre?.Name,
                ISBN = book.ISBN
            };
        }

        public BookEditViewModel ConvertBookEdit(Book book)
        {
            var vm = new BookEditViewModel
            {
                Id = book.Id,
                Title = book.Title,
                CreationDate = book.CreationDate,
                Genre = book.Genre?.Name,
                GenreId = book.Genre?.Id,
                AuthorId1 = (book.Authors.Count != 0) ? book.Authors?[0].Author.Id : 0,
                AuthorId2 = (book.Authors.Count > 1) ? book.Authors?[1].Author.Id : 0
            };
            return vm;
        }

    }
}
