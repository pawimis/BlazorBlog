using BlazorBlog.Shared.Entities;

using System;
using System.Collections.Generic;

namespace BlazorBlog.TestFake
{
    public class TestData
    {
        public static List<BlogPost> BlogPosts { get; private set; }
        public static void CreateTestBlogPosts()
        {
            BlogPosts = new List<BlogPost>();
            BlogPost post = new BlogPost
            {
                BranchVersion = 0.1,
                CreateDate = DateTime.Now,
                GithubLink = "https://github.com/pawimis/BlazorBlog/tree/v0.1",
                Id = 0,
                Title = "First Post",
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
        }
    }
}
