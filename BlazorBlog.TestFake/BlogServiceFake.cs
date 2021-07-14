using BlazorBlog.Shared.Contracts;
using BlazorBlog.Shared.Entities;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBlog.TestFake
{
    public class BlogServiceFake : IBlogService
    {
        public Task<List<BlogPost>> GetAllPosts()
        {
            TestData.CreateTestBlogPosts();
            return Task.FromResult(TestData.BlogPosts);
        }

        public Task<BlogPost> GetPostWithId(int id)
        {
            if (TestData.BlogPosts == null)
            {
                TestData.CreateTestBlogPosts();
            }

            return Task.FromResult(TestData.BlogPosts.FirstOrDefault(x => x.Id == id));
        }

        public Task InsertNewPost(BlogPost post)
        {
            TestData.BlogPosts.Add(post);
            return Task.FromResult(true);
        }


    }
}
