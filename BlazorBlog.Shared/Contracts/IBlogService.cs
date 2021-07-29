using BlazorBlog.Shared.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorBlog.Shared.Contracts
{
    public interface IBlogService
    {
        Task InsertNewPost(BlogPostEntityDTO post);
        Task<List<BlogPostEntityDTO>> GetAllPosts();
        Task<BlogPostEntityDTO> GetPostWithId(int id);
        Task<List<TagEntityDTO>> GetAllTags();
    }
}
