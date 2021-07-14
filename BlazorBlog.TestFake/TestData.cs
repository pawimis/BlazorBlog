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
                Id = 0,
                Title = "First Post",
                FrontPostImage = "https://i0.wp.com/xamgirl.com/wp-content/uploads/2021/06/Screen-Shot-2021-06-02-at-3.17.33-PM.png?resize=665%2C435&ssl=1",
                IntroContent = "This is my first post on this blog. Right now it is really simple and it has minimum functionality ",
                Content = "It is alive! :) <br/> I started learning Blazor from Udemy course and after ~20 hours I am able to create simple webpage. It is not perfect but I will work on that <br/>" +
                "I hope that I will be able to add and describe some of the actions taken to create this. Right now it is realy hard to write anything usefull because of lackage of post adding functionality.",
                Tags = new List<string> { "intro", "test", "firstPost" }
            };
            BlogPosts.Add(post);

            BlogPost post2 = CopyAndUpdatePropeties(post);
            BlogPosts.Add(post2);

            BlogPost post3 = CopyAndUpdatePropeties(post2);
            BlogPosts.Add(post3);

        }

        private static BlogPost CopyAndUpdatePropeties(BlogPost post)
        {
            BlogPost nextPost = new BlogPost(post);
            nextPost.Id++;
            nextPost.CreateDate = nextPost.CreateDate.AddDays(1);
            return nextPost;
        }
    }
}
