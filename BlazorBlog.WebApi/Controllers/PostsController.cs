using AutoMapper;

using BlazorBlog.Shared.Entities;
using BlazorBlog.WebApi.Contracts;
using BlazorBlog.WebApi.Data.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;


        public PostsController(IPostRepository postRepository, ILoggerService loggerService, IMapper mapper, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _loggerService = loggerService;
            _mapper = mapper;
            _tagRepository = tagRepository;
        }
        /// <summary>
        /// Get All Posts
        /// </summary>
        /// <returns> List of posts</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                IList<Data.Entities.BlogPost> posts = await _postRepository.FindAll();
                IList<BlogPostEntityCreateDTO> response = _mapper.Map<IList<BlogPostEntityCreateDTO>>(posts);
                return Ok(response);
            }
            catch (System.Exception e)
            {
                return InternalError(e);
            }

        }
        /// <summary>
        /// Gets a post with id
        /// </summary>
        /// <param name="id">id of post</param>
        /// <returns>post or null if not found</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPost(int id)
        {
            try
            {
                Data.Entities.BlogPost post = await _postRepository.FindById(id);
                if (post == null)
                {
                    return NotFound();
                }
                BlogPostEntityDTO response = _mapper.Map<BlogPostEntityDTO>(post);
                return Ok(response);
            }
            catch (System.Exception e)
            {
                return InternalError(e);
            }
        }
        /// <summary>
        /// Creates post
        /// </summary>
        /// <param name="postDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] BlogPostEntityCreateDTO postDTO)
        {
            try
            {
                if (postDTO == null)
                {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                BlogPost post = _mapper.Map<BlogPost>(postDTO);
                post.CreateDate = DateTime.Now;
                int postId = await _postRepository.Create(post);

                return Ok(postId > 0);
            }
            catch (System.Exception e)
            {
                return InternalError(e);
            }
        }

        /// <summary>
        /// Updates post
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] BlogPostEntityCreateDTO postDTO)
        {
            try
            {
                if (id < 1 || postDTO == null || id != postDTO.Id)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                BlogPost post = _mapper.Map<BlogPost>(postDTO);
                bool isSuccess = await _postRepository.Update(post);
                if (!isSuccess)
                {
                    return InternalError(new Exception("Update Operation Error"));
                }
                return NoContent();
            }
            catch (System.Exception e)
            {
                return InternalError(e);
            }
        }
        /// <summary>
        /// Removes a post by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }

                bool exists = await _postRepository.Exists(id);
                if (!exists)
                {
                    return NotFound();
                }
                Data.Entities.BlogPost post = await _postRepository.FindById(id);
                bool isSuccess = await _postRepository.Delete(post);
                if (!isSuccess)
                {
                    return InternalError(new Exception("Delete Operation Error"));
                }
                return NoContent();

            }

            catch (Exception)
            {

                throw;
            }
        }
        private IActionResult InternalError(Exception e)
        {
            _loggerService.LogError($"{e.Message} - {e.InnerException}");
            return StatusCode(500, "Something went wrong");
        }
    }
}
