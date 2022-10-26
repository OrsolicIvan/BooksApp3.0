using System;

namespace Books.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository Authors { get; }
        IAppUserRepository Users { get; }
        IBookRepository Books { get; }
        IImageService ImageService { get; }
        int Complete();
    }
}
