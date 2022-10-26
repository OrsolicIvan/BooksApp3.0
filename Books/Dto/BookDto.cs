namespace Books.Dto
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int BookYear { get; set; }
        public ImageDto Images { get; set; }
    }
}
