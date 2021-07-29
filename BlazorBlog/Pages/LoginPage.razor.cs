using BlazorBlog.BlazorBlogAuthStateProvider;
using BlazorBlog.Entities;
using BlazorBlog.Shared.Contracts;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using System;
using System.Linq;
using System.Linq.Expressions;

namespace BlazorBlog.Pages
{
    public partial class LoginPage : ComponentBase
    {
        private User User { get; set; } = new User();
        private EditContext EditContext { get; set; }
        [Inject]
        private IUserManager UserManager { get; set; }
        [Inject]
        private IAuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContext = new EditContext(User);

        }
        public string GetError(Expression<Func<object>> fu)
        {
            if (EditContext == null)
            {
                return null;
            }
            return EditContext.GetValidationMessages(fu).FirstOrDefault();
        }
        protected async void OnSubmit()
        {
            if (!EditContext.Validate())
            {
                return;
            }
            try
            {
                Shared.Entities.UserDTO loggedUser = await UserManager.TrySignInAndGetUserAsync(User);
                if (loggedUser != null)
                {
                    AuthenticationStateProvider.SetAuthenticatedState(loggedUser);
                    NavigationManager.NavigateTo("newpost");
                }
                else
                {
                }
            }
            catch (Exception e)
            {
            }

        }
    }
}
