using BlazorBlog.BlazorBlogAuthStateProvider;
using BlazorBlog.Business;
using BlazorBlog.Shared.Contracts;
using BlazorBlog.WebAPIAccess;

using Blazored.LocalStorage;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorBlog
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped(sp =>
                   new HttpClient { BaseAddress = new Uri(builder.Configuration["apiAddress"]) });
            builder.Services.AddScoped<IBlogService, WebAPIAccess.BlogService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddScoped<IUserManager, UserManager>();
            builder.Services.AddScoped<WebAPIAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthenticationStateProvider>(
                    provider => provider.GetRequiredService<WebAPIAuthenticationStateProvider>());
            builder.Services.AddScoped<AuthenticationStateProvider>(
                provider => provider.GetRequiredService<WebAPIAuthenticationStateProvider>());
            await builder.Build().RunAsync();
        }
    }
}
