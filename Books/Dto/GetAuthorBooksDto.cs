using System.Collections.Generic;

namespace Books.Dto
{
    public class GetAuthorBooksDto
    {
        public string AuthorName { get; set; }
        public string AuthorBirthYear { get; set; }
        public List<string> BookTitles { get; set; }
    }
}
