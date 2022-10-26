using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Dto
{
    public class AddBookDto
    {
       
        public string BookTitle { get; set; }

        [Column(TypeName = "nvarchar(4)")]
        public int BookYear { get; set; }
        public int OwnerId { get; set; }
        public string Url { get; set; }

    }
}
