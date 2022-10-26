namespace Books.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
    }
}
