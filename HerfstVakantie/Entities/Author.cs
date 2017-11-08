using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerfstVakantie.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public virtual List<BookAuthor> Books { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
    }
}
