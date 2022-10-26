using AutoMapper;
using Books.Dto;
using Books.Models;

namespace Books.Data
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap();
        }
        private void CreateMap()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();
            CreateMap<AppUser, GetAppUserDto>().ReverseMap();
            CreateMap<AddAuthorDto, Author>().ReverseMap();
            CreateMap<GetAuthorDto, Author>().ReverseMap();
            CreateMap<AuthorUpdateDto, Author>().ReverseMap();
            CreateMap<AppUser, RegisterDto>().ReverseMap();
            CreateMap<AppUserDto, RegisterDto>().ReverseMap();
            CreateMap<Book, GetBookDto>().ReverseMap();
            CreateMap<Book, AllBookDto>().ReverseMap();
            CreateMap<AppUser, RentBookDto>().ReverseMap();
            CreateMap<Book, RentBookDto>().ReverseMap();
            CreateMap<GetBookAuthorDto,Book_Author>().ReverseMap();
            CreateMap<GetBookWithAuthorDto, Book>().ReverseMap();
            CreateMap<BookUpdateDto, Book>().ReverseMap();
            CreateMap<AddBookDto, Book>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<BookDto, Book>().ReverseMap();
            CreateMap<GetBookDto, BookDto>().ReverseMap();
            CreateMap<Image, ImageDto>().ReverseMap();

        }

    }
}
