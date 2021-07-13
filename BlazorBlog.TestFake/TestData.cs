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
                Title = "TestEntry",
                FrontPostImage = "https://i0.wp.com/xamgirl.com/wp-content/uploads/2021/06/Screen-Shot-2021-06-02-at-3.17.33-PM.png?resize=665%2C435&ssl=1",
                IntroContent = "Have you ever wondered how to pass multiple parameters to a converter, or how to create properties in a converter to change the value based on them? If you want to know how to achieve it then keep reading :).",
                Content = "Have you ever wondered how to pass multiple parameters to a converter, or how to create properties in a converter to change the value based on them? If you want to know how to achieve it then keep reading :)." +
                 "Have you ever wondered how to pass multiple parameters to a converter, or how to create properties in a converter to change the value based on them? If you want to know how to achieve it then keep reading :)." +
                 "Have you ever wondered how to pass multiple parameters to a converter, or how to create properties in a converter to change the value based on them? If you want to know how to achieve it then keep reading :).",
                Tags = new List<string> { "intro", "test", "firstPost" }
            };
            BlogPosts.Add(post);
            BlogPosts.Add(post);
            BlogPosts.Add(post);
        }
    }
}
