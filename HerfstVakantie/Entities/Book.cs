using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerfstVakantie.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual Genre Genre { get; set; }
        public string ISBN { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual List<BookAuthor> Authors { get; set; }
    }
}
