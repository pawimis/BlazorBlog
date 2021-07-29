using BlazorBlog.Shared.Entities;

using System.Threading.Tasks;

namespace BlazorBlog.Shared.Contracts
{
    public interface IUserManager
    {
        Task<UserDTO> TrySignInAndGetUserAsync(UserDTO user);
    }
}
