using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string BookTitle { get; set; }

        [Column(TypeName = "nvarchar(4)")]
        public int BookYear { get; set; }
        public AppUser Renter { get; set; }
        public int? RenterId { get; set; }
        public int OwnerId { get; set; }
        public AppUser Owner { get; set; }
        public List<Book_Author> Book_Authors { get; set; }
        public Image Images { get; set; }

    }
}
