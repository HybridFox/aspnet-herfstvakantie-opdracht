using HerfstVakantie.Entities;
using HerfstVakantie.Models;
using HerfstVakantie.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerfstVakantie.Controllers
{
    public class BookAPIController : Controller
    {
        private readonly IBookService _bookService;

        public BookAPIController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("/api/books")]
        public IActionResult Index([FromQuery] BookCriteria criteria)
        {
            var books = _bookService.GetAllBooksSortedByName(criteria)
                .Select(x => ConvertBookDetail(x)).ToList();

            return Ok(books);
        }

        private static BookDetailViewModel ConvertBookDetail(Book book)
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
    }
}
