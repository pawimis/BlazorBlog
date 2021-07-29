using BlazorBlog.WebApi.Contracts;
using BlazorBlog.WebApi.Data.Entities;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.IO;
using System.Threading.Tasks;

namespace BlazorBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileRepository _context;
        private readonly IWebHostEnvironment _env;
        private readonly ILoggerService _loggerService;

        public FilesController(IFileRepository context, IWebHostEnvironment env, ILoggerService loggerService)
        {
            _context = context;
            _env = env;
            _loggerService = loggerService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                FileDetail fileDetail = new();
                string fileType = Path.GetExtension(file.FileName);
                string fileUrl = string.Empty;
                if (fileType.ToLower() == ".jpg" || fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg")
                {
                    string filePath = _env.ContentRootPath;
                    string docName = Path.GetFileName(file.FileName);
                    if (file != null && file.Length > 0)
                    {
                        fileDetail.Id = Guid.NewGuid();
                        fileDetail.DateCreated = DateTime.Now;
                        fileDetail.DocumentName = docName;
                        fileDetail.DocType = fileType;
                        fileDetail.DocUrl = Path.Combine(filePath, "Files", fileDetail.Id.ToString() + fileDetail.DocType);
                        using (FileStream stream = new(fileDetail.DocUrl, FileMode.Create, FileAccess.Write))
                        {
                            await file.CopyToAsync(stream);
                        }
                        await _context.Create(fileDetail);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                return Ok(fileUrl);
            }
            catch (Exception e)
            {
                return InternalError(e);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Download(Guid id)
        {
            try
            {
                FileDetail fileDetail = await _context.FindById(id);

                if (fileDetail != null)
                {
                    System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                    {
                        FileName = fileDetail.DocumentName,
                        Inline = false
                    };
                    Response.Headers.Add("Content-Disposition", cd.ToString());

                    //get physical path
                    string path = _env.ContentRootPath;
                    string fileReadPath = Path.Combine(path, "Files", fileDetail.Id.ToString() + fileDetail.DocType);

                    FileStream file = System.IO.File.OpenRead(fileReadPath);
                    return File(file, fileDetail.DocType);
                }
                else
                {
                    return StatusCode(404);
                }
            }
            catch (Exception e)
            {

                return InternalError(e);
            }
        }
        private IActionResult InternalError(Exception e)
        {
            _loggerService.LogError($"{e.Message} - {e.InnerException}");
            return StatusCode(500, "Something went wrong");
        }
    }
}
