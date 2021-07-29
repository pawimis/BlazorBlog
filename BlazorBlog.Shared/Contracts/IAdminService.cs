using BlazorBlog.Shared.Entities;

using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorBlog.Shared.Contracts
{
    public interface IAdminService
    {
        Task<UserDTO> Login(UserDTO user);
        Task<string> UploadImage(MultipartFormDataContent file);
        Task Logout();
        Task<UserDTO> GetUserByTokenAsync();
    }
}
