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
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public TagsController(ITagRepository tagRepository, ILoggerService loggerService, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _loggerService = loggerService;
            _mapper = mapper;
        }


        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTags()
        {
            try
            {
                IList<PostTag> tags = await _tagRepository.FindAll();
                IList<TagEntityDTO> response = _mapper.Map<IList<TagEntityDTO>>(tags);
                return Ok(response);
            }
            catch (System.Exception e)
            {
                return InternalError(e);
            }

        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTag(string key)
        {
            try
            {
                PostTag tag = await _tagRepository.FindByKey(key);
                if (tag == null)
                {
                    return NotFound();
                }
                TagEntityDTO response = _mapper.Map<TagEntityDTO>(tag);
                return Ok(response);
            }
            catch (System.Exception e)
            {
                return InternalError(e);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] TagEntityCreateDTO tagDTO)
        {
            try
            {
                if (tagDTO == null)
                {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                PostTag tag = _mapper.Map<PostTag>(tagDTO);
                bool isSuccess = await _tagRepository.Create(tag) > 0;
                if (!isSuccess)
                {
                    return InternalError(new Exception("Tag Creation Error"));
                }
                return Ok(isSuccess);
            }
            catch (System.Exception e)
            {
                return InternalError(e);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(string key, [FromBody] TagEntityDTO tagDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(tagDTO.TagText))
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                PostTag tag = _mapper.Map<PostTag>(tagDTO);
                bool isSuccess = await _tagRepository.Update(tag);
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

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string key)
        {
            try
            {
                if (key == null)
                {
                    return BadRequest();
                }

                bool exists = await _tagRepository.Exists(key);
                if (!exists)
                {
                    return NotFound();
                }
                PostTag tag = await _tagRepository.FindByKey(key);
                bool isSuccess = await _tagRepository.Delete(tag);
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
