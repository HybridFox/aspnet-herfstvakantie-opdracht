using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HerfstVakantie.Models
{
    public class BookEditViewModel
    {
        [Required]
        public string Title { get; set; }
        [RegularExpression(@"^(97(8|9))?\d{9}(\d|X)$")]
        public string ISBN { get; set; }
        public DateTime CreationDate { get; set; }
        public int Id { get; set; }
        public string Genre { get; set; }
        public int? GenreId { get; set; }
        public string Author1 { get; set; }
        public int? AuthorId1 { get; set; }
        public string Author2 { get; set; }
        public int? AuthorId2 { get; set; }
        public List<SelectListItem> Genres { get; set; }
        public List<SelectListItem> Authors { get; set; }
    }
}
