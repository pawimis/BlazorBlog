using BlazorBlog.Shared.Entities;

using System.Threading.Tasks;

namespace BlazorBlog.Shared.Contracts
{
    public interface IAdminService
    {
        Task<UserDTO> Login(UserDTO user);
        Task Logout();
        Task<UserDTO> GetUserByTokenAsync();
    }
}
