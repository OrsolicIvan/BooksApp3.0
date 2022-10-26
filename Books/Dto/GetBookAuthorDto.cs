namespace Books.Dto
{
    public class GetBookAuthorDto
    {
        public GetAuthorDto Author { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
    }
}
