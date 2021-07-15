using AutoMapper;

using BlazorBlog.Shared.Entities;
using BlazorBlog.WebApi.Contracts;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorBlog.WebApi.Controllers
{
    /// <summary>
    /// Endpoint used to interact with posts 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;


        public PostsController(IPostRepository postRepository, ILoggerService loggerService, IMapper mapper)
        {
            _postRepository = postRepository;
            _loggerService = loggerService;
            _mapper = mapper;
        }
        /// <summary>
        /// Get All Posts
        /// </summary>
        /// <returns> List of posts</returns>
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                IList<Data.Entities.BlogPost> posts = await _postRepository.FindAll();

                IList<BlogPostEntityDTO> response = _mapper.Map<IList<BlogPostEntityDTO>>(posts);
                return Ok(response);
            }
            catch (System.Exception e)
            {
                _loggerService.LogError($"{e.Message}");

                return StatusCode(500, "Something went wrong");
            }

        }
    }
}
