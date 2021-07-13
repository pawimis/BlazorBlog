using BlazorBlog.Shared.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorBlog.Shared.Contracts
{
    public interface IBlogService
    {
        Task InsertNewPost(BlogPost post);
        Task<List<BlogPost>> GetAllPosts();
        Task<BlogPost> GetPostWithId(int id);
    }
}
