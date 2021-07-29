using BlazorBlog.Shared.Entities;

using Microsoft.AspNetCore.Components;

namespace BlazorBlog.Components
{
    public partial class PostComponent : ComponentBase
    {
        [Parameter]
        public BlogPostEntityDTO Post { get; set; }
    }
}
