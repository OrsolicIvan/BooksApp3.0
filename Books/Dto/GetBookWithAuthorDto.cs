using System.Collections.Generic;

namespace Books.Dto
{
    public class GetBookWithAuthorDto
    {
        public int BookId { get; set; }
        public int OwnerId { get; set; }
        public GetAppUserDto Owner { get; set; }
        public int RenterId { get; set; }
        public GetAppUserDto Renter { get; set; }
        public string BookTitle { get; set; }
        public int BookYear { get; set; }
        public List<GetBookAuthorDto> Book_Authors{get;set;}
        public ImageDto Images { get; set; }
    }
}
