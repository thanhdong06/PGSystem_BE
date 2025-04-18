﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGSystem.ResponseType;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Service.Blogs;
using System.Security.Claims;



namespace PGSystem.Controllers
{
    [Route("api/Blog")]
    [ApiController]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await _blogService.GetAllBlogsAsync();
            return Ok(blogs);
        }

        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] BlogRequest request)
        {
            try
            {
                var blog = await _blogService.CreateBlogAsync(request, User);
                return CreatedAtAction(nameof(CreateBlog), new { id = blog.BID }, blog);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(Roles = "Member")]
        [HttpDelete("{bid}")]
        public async Task<ActionResult<JsonResponse<string>>> DeleteBlog(int bid)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User is not authenticated" });
            var result = await _blogService.DeleteBlogsAsync(bid, userId);

            if (!result)
                return NotFound(new { message = "Blog does not exist or you don't have permission to delete it!" });

            return Ok(new { message = "Blog and all comments have been deleted" });
        }


        [HttpGet("GetAllByAuthor/{aid}")]
        public async Task<IActionResult> GetAllBlogByAID(int aid)
        {

            var blog = await _blogService.GetAllBlogByAID(aid);

            if (blog == null || !blog.Any())
            {
                return NotFound(new JsonResponse<List<BlogResponse>>(new List<BlogResponse>(), StatusCodes.Status404NotFound, "There are no blogs by this author"));
            }

            return Ok(blog);
        }
        [HttpGet("{bid}")]
        public async Task<IActionResult> GetBlogByBID(int bid)
        {
            var blog = await _blogService.GetBlogByBIDAsync(bid);
            if (blog == null)
            {
                return NotFound($"Blog with BID {bid} does not exist.");
            }
            return Ok(blog);
        }
        [HttpPut("{bid}")]
        public async Task<IActionResult> UpdateBlog(int bid, [FromBody] BlogUpdateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid data");
            }

            var updatedBlog = await _blogService.UpdateBlogAsync(bid, request);
            if (updatedBlog == null)
            {
                return NotFound($"Blog with BID {bid} does not exist.");
            }

            return Ok(updatedBlog);
        }
    }
}
