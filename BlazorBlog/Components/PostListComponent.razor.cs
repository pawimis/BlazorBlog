using BlazorBlog.Shared.Contracts;
using BlazorBlog.Shared.Entities;

using Microsoft.AspNetCore.Components;

using System.Collections.ObjectModel;
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
            BlogPostItems = new ObservableCollection<BlogPostEntityDTO>(await BlogService.GetAllPosts());
        }
    }
}
