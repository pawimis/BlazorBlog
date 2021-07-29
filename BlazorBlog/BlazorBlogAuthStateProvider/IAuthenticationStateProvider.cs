using BlazorBlog.Shared.Entities;

namespace BlazorBlog.BlazorBlogAuthStateProvider
{
    internal interface IAuthenticationStateProvider
    {
        void SetAuthenticatedState(UserDTO user);
        void UnsetUser();
    }
}
