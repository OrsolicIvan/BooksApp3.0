using Books.Models;
using System.Collections.Generic;

namespace Books.Dto
{
    public class GetBookDto
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int BookYear { get; set; }
        public int RenterId { get; set; }
        public GetAppUserDto Renter { get; set; }
        public int OwnerId { get; set; }
        public GetAppUserDto Owner { get; set; }
        public List<GetBookAuthorDto> Book_Authors { get; set; }
        public ImageDto Images { get; set; }
    }
}
