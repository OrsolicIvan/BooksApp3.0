using Books.Models;
using System.Threading.Tasks;

namespace Books.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
