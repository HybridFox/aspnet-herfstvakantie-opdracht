using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HerfstVakantie.Entities;

namespace HerfstVakantie.Models
{
    public class BookDetailViewModel
    {
        public string ISBN;
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
        public int Id { get; set; }
        public string Genre { get; set; }
    }
}
