using BlazorBlog.Shared.Contracts;
using BlazorBlog.Shared.Entities;

using Microsoft.AspNetCore.Components;

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBlog.Components
{
    public partial class PostListComponent : ComponentBase
    {
        [Inject]
        private IBlogService BlogService { get; set; }

        protected ObservableCollection<BlogPostEntityDTO> BlogPostItems { get; set; } = new ObservableCollection<BlogPostEntityDTO>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            ObservableCollection<BlogPostEntityDTO> posts = new(await BlogService.GetAllPosts());
            if (posts.Any())
            {
                BlogPostItems = new ObservableCollection<BlogPostEntityDTO>(posts.OrderByDescending(p => p.CreateDate));
            }
        }
    }
}
