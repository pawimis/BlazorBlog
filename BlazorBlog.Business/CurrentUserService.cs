using BlazorBlog.Shared.Contracts;
using BlazorBlog.Shared.Entities;

namespace BlazorBlog.Business
{
    public class CurrentUserService : ICurrentUserService
    {
        public UserDTO CurrentUser { get; set; }
    }
}
