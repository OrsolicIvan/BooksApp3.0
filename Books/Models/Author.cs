using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        public string AuthorName { get; set; }
        public List<Book_Author> Book_Authors { get; set; }
    }
}
