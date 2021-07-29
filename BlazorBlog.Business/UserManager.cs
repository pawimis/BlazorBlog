using BlazorBlog.Shared.Contracts;
using BlazorBlog.Shared.Entities;

using System.Threading.Tasks;

namespace BlazorBlog.Business
{
    public class UserManager : IUserManager
    {
        private IAdminService _adminService;

        public UserManager(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<UserDTO> TrySignInAndGetUserAsync(UserDTO user)
        {
            return await _adminService.Login(user);
        }
    }
}
