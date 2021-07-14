using BlazorBlog.Shared.Entities;

using Microsoft.AspNetCore.Components;

using System;

namespace BlazorBlog.Components
{
    public partial class PostPreviewComponent<TItem> : ComponentBase where TItem : BlogPost
    {
        [Parameter]
        public TItem Item { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        private void OpenPostItem()
        {
            Uri.TryCreate("/post/" + Item.Id,
                UriKind.Relative, out Uri uri);
            NavigationManager.NavigateTo(uri.ToString());
        }
    }
}
