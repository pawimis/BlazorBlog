
using BlazorBlog.Shared.Entities;

using Microsoft.AspNetCore.Components;

namespace BlazorBlog.Pages
{
    public partial class MainPage : ComponentBase
    {

        protected AboutEntity AboutMeItem { get; private set; }
        protected AboutEntity AboutBlog { get; private set; }
        protected AboutEntity ToYou { get; private set; }

        protected override void OnInitialized()
        {
            AboutMeItem = new AboutEntity { Title = "About Me", Content = "I'm Paweł <br/> I have about 5 years of experience in .NET and 4 years of commercial experience in Xamarin.Forms" };
            AboutBlog = new AboutEntity { Title = "About Blog", Content = "This is an experiment. I would like to learn Blazor, HTML, CSS and other web stuff. Since the best way (for me) to learn is to use. I want to make a blog about creating a blog in Blazor and deploy it into Azure." };
            ToYou = new AboutEntity { Title = "To You", Content = "If you would like to ask a question (comments section will probably appear in future), suggest that something should be done in a better way or you found a bug, feel free to contact me with e-mail: pawimis@gmail.com" };
            base.OnInitialized();
        }

    }
}
