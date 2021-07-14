using BlazorBlog.Shared.Contracts;
using BlazorBlog.Shared.Entities;

using Microsoft.AspNetCore.Components;

using System;
using System.Threading.Tasks;

namespace BlazorBlog.Pages
{
    public partial class PostPage : ComponentBase
    {
        private BlogPost Post { get; set; } = new BlogPost();
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IBlogService BlogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await SetDataFromUri();
        }
        private async Task SetDataFromUri()
        {
            Uri uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            int segmentCount = uri.Segments.Length;
            if (segmentCount > 2)
            {
                if (segmentCount > 1 && int.TryParse(uri.Segments[segmentCount - 1], out int Id))
                {
                    BlogPost post = await BlogService.GetPostWithId(Id);
                    if (post == null)
                    {
                        NavigationManager.NavigateTo("/");
                    }
                    else
                    {
                        Post = post;
                        StateHasChanged();
                    }
                }
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
