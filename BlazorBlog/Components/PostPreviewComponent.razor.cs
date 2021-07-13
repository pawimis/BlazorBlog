using BlazorBlog.Shared.Entities;

using Microsoft.AspNetCore.Components;

namespace BlazorBlog.Components
{
    public partial class PostPreviewComponent<TItem> : ComponentBase where TItem : BlogPost
    {
        [Parameter]
        public TItem Item { get; set; }
    }
}
