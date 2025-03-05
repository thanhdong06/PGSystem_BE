using Microsoft.AspNetCore.Mvc;
using PGSystem.ResponseType;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_Service.Blogs;



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

        [HttpPost("Create")]
        public async Task<IActionResult> CreateBlog([FromBody] BlogRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid Data");
            }

            var createdBlog = await _blogService.CreateBlogAsync(request);
            return Ok(createdBlog);
        }
        [HttpDelete]
        public async Task<ActionResult<JsonResponse<string>>> DeleteBlog(int bid)
        {
            try
            {
                var isDeleted = await _blogService.DeleteBlogsAsync(bid);

                if (!isDeleted)
                {
                    return NotFound(new JsonResponse<string>(null, 404, "blog not found"));
                }

                return Ok(new JsonResponse<string>(null, 200, "Blog deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse<string>(null, 500, "An error occurred while deleting the blog"));
            }
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
        public async Task<IActionResult> UpdateBlog(int bid, [FromBody] BlogRequest request)
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
