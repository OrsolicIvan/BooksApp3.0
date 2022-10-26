using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Dto
{
    public class AuthorDto
    {
        [Key]
        public int AuthorId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string AuthorName { get; set; }
        [Column(TypeName = "nvarchar(4)")]
        public string AuthorBirthYear { get; set; }
    }
}
