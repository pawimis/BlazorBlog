using BlazorBlog.Shared.Entities;

using Microsoft.AspNetCore.Components;

namespace BlazorBlog.Components
{
    public partial class AboutComponent<TItem> : ComponentBase where TItem : AboutEntityDTO
    {
        [Parameter]
        public TItem Item { get; set; }

    }
}
