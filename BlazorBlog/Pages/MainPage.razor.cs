
using BlazorBlog.Shared.Entities;

using Microsoft.AspNetCore.Components;

namespace BlazorBlog.Pages
{
    public partial class MainPage : ComponentBase
    {

        protected AboutEntityDTO AboutMeItem { get; private set; }
        protected AboutEntityDTO AboutBlog { get; private set; }

        protected override void OnInitialized()
        {
            AboutMeItem = new AboutEntityDTO { Title = "About Me", Content = "I'm Paweł <br/> I have about 5 years of experience in .NET and 4 years of comertial experience in Xamarin.Forms" };
            AboutBlog = new AboutEntityDTO { Title = "About Blog", Content = "This is an experiment. I would like to learn Blazor, html, css and other web stuff. Since the best way (for me) to learn is to use. I want to make a blog about creating a blog in Blazor and deploy it into Azure." };
            base.OnInitialized();
        }

    }
}
