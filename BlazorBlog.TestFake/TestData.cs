using BlazorBlog.Shared.Entities;

using System;
using System.Collections.Generic;

namespace BlazorBlog.TestFake
{
    public class TestData
    {
        public static List<BlogPostEntityDTO> BlogPosts { get; private set; }
        public static void CreateTestBlogPosts()
        {
            BlogPosts = new List<BlogPostEntityDTO>();

            BlogPostEntityDTO post = new BlogPostEntityDTO
            {
                BranchVersion = 0.1,
                CreateDate = DateTime.Now,
                GithubLink = "https://github.com/pawimis/BlazorBlog/tree/v0.1",
                Id = 0,
                Title = "First Post",
<<<<<<< HEAD
                FrontPostImage = "https://i0.wp.com/xamgirl.com/wp-content/uploads/2021/06/Screen-Shot-2021-06-02-at-3.17.33-PM.png?resize=665%2C435&ssl=1",
                IntroPostContent = "This is my first post on this blog. Right now it is really simple and it has minimum functionality ",
                PostContent = "It is alive! :) <br/> I started learning Blazor from Udemy course and after ~20 hours I am able to create simple webpage. It is not perfect but I will work on that <br/>" +
                "I hope that I will be able to add and describe some of the actions taken to create this. Right now it is realy hard to write anything usefull because of lackage of post adding functionality.",
                Tags = new List<TagEntityDTO> { new TagEntityDTO(0, "intro"), new TagEntityDTO(1, "intro") }
            };
            BlogPosts.Add(post);

            BlogPostEntityDTO post2 = CopyAndUpdatePropeties(post);
            BlogPosts.Add(post2);

            BlogPostEntityDTO post3 = CopyAndUpdatePropeties(post2);
            BlogPosts.Add(post3);

        }

        private static BlogPostEntityDTO CopyAndUpdatePropeties(BlogPostEntityDTO post)
        {
            BlogPostEntityDTO nextPost = new BlogPostEntityDTO(post);
            nextPost.Id++;
            nextPost.CreateDate = nextPost.CreateDate.AddDays(1);
            return nextPost;
=======
                FrontPostImage = "/images/firstImage.png",
                IntroContent = "This is my first post on this blog. Right now it is really simple and it has minimum functionality ",
                Content = "It is alive! :) <br/> I started learning Blazor from Udemy course and after ~20 hours I am able to create simple webpage. It is not perfect, but I will work on that <br/>" +
                "I hope that I will be able to add and describe some of the actions taken to create this. Right now it is really hard to write anything useful because of the lack of a handy post adding functionality." +
                "For now I am closing version 0.1. Link: https://github.com/pawimis/BlazorBlog/tree/v0.1 <br/>" +
                "I will deal with further corrections in the near future. The first step will be to create and host ASP.NET Core API with Entity framework to store posts." +
                " Then I will create a page for adding posts. I think this is a good starting point before describing the steps I took to create this blog ",
                Tags = new List<string> { "intro", "info" }
            };
            BlogPosts.Add(post);
>>>>>>> a5309edebb0cfa6f89415cd769414c2c3dccd5a1
        }
    }
}
