using BlazorBlog.Shared.Entities;

namespace BlazorBlog.Shared.Contracts
{
    public interface ICurrentUserService
    {
        UserDTO CurrentUser { get; set; }

    }
}
