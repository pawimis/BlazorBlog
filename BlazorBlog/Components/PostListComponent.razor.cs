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

        protected ObservableCollection<BlogPost> BlogPostItems { get; set; } = new ObservableCollection<BlogPost>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            BlogPostItems = new ObservableCollection<BlogPost>(await BlogService.GetAllPosts());
        }
    }
}
