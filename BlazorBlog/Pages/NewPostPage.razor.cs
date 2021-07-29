
using BlazorBlog.Shared.Contracts;
using BlazorBlog.Shared.Entities;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBlog.Pages
{
    public partial class NewPostPage : ComponentBase
    {
        private bool isLoading;
        private List<IBrowserFile> loadedFiles = new();
        private long maxFileSize = 1024 * 15;
        private int maxAllowedFiles = 1;
        private List<TagEntityDTO> _tagList { get; set; } = new List<TagEntityDTO>();
        private List<string> SelectedTagList { get; set; } = new List<string>();
        private string NewTagText { get; set; }
        private bool IsAuthenticated { get; set; } = false;
        private bool ShowPreview { get; set; } = false;
        private BlogPostEntityDTO Post { get; set; } = new BlogPostEntityDTO();
        [Inject]
        private IBlogService BlogService { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();

        }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _tagList = await BlogService.GetAllTags();
            Console.WriteLine(_tagList.Any());
            StateHasChanged();

        }
        private void OnItemClicked(TagEntityDTO item)
        {
            if (item != null)
            {
                if (SelectedTagList.Contains(item.TagText))
                {
                    return;
                }
                SelectedTagList.Add(item.TagText);
                Console.WriteLine("Added " + item.TagText);
            }
            else
            {
                Console.WriteLine("Not Added ");

            }

            StateHasChanged();
        }
        private void AddTag()
        {
            if (!string.IsNullOrWhiteSpace(NewTagText))
            {
                if (SelectedTagList.Contains(NewTagText))
                {
                    return;
                }

                SelectedTagList.Add(NewTagText);
                StateHasChanged();
            }

        }
        private void OnTagRemove(string item)
        {
            SelectedTagList.Remove(item);
            StateHasChanged();

        }
        private void SendForm()
        {

        }
        private void GeneratePreview()
        {
            if (SelectedTagList.Any())
            {
                if (Post.Tags == null)
                {
                    Post.Tags = new List<TagEntityDTO>();
                }
                else
                {
                    Post.Tags.Clear();
                }

                foreach (string item in SelectedTagList)
                {
                    Post.Tags.Add(new TagEntityDTO { TagText = item });
                }
            }
            ShowPreview = true;
            StateHasChanged();

        }
        protected override async Task OnParametersSetAsync()
        {


            await base.OnParametersSetAsync();
            AuthenticationState authState = await AuthenticationStateTask;
            IsAuthenticated = authState.User.Identity.IsAuthenticated;

            if (!IsAuthenticated)
            {
                return;
            }
            try
            {


            }
            finally
            {
            }
        }
        private void LoadFiles(InputFileChangeEventArgs e)
        {
            isLoading = true;
            loadedFiles.Clear();

            foreach (IBrowserFile file in e.GetMultipleFiles(maxAllowedFiles))
            {
                try
                {
                    loadedFiles.Add(file);
                }
                catch (Exception ex)
                {

                }
            }

            isLoading = false;
        }
    }
}
