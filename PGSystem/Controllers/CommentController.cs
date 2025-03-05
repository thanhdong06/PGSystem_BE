using Microsoft.AspNetCore.Mvc;
using PGSystem.ResponseType;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Service.Blogs;
using PGSystem_Service.Comments;

namespace PGSystem.Controllers
{
    [Route("api/Comment")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            if (comments == null)
            {
                return NotFound(new JsonResponse<List<CommentResponse>>(new List<CommentResponse>(), StatusCodes.Status404NotFound, "No Comment"));
            }
            return Ok(comments);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateComment([FromBody] CommentRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid Data");
            }

            var createdComment = await _commentService.CreateCommentAsync(request);
            return Ok(createdComment);
        }
        [HttpDelete]
        public async Task<ActionResult<JsonResponse<string>>> DeleteComment(int cid)
        {
            try
            {
                var isDeleted = await _commentService.DeleteCommentAsync(cid);

                if (!isDeleted)
                {
                    return NotFound(new JsonResponse<string>(null, 404, "comment not found"));
                }

                return Ok(new JsonResponse<string>(null, 200, "Comment deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new JsonResponse<string>(null, 500, "An error occurred while deleting the comment"));
            }
        }
        [HttpGet("Comment/{BID}")]
        public async Task<IActionResult> GetAllCommentByBlogId(int bid)
        {
            var comments = _commentService.GetAllCommentByBID(bid);
            if (comments == null)
            {
                return NotFound(new JsonResponse<List<CommentResponse>>(new List<CommentResponse>(), StatusCodes.Status404NotFound, "No Comment"));
            }
            return Ok(comments);
        }
        [HttpGet("{cid}")]
        public async Task<IActionResult> GetCommentByCID(int cid)
        {
            var comment = await _commentService.GetCommentByCIDAsync(cid);
            if (comment == null)
            {
                return NotFound($"Comment với CID {cid} không tồn tại.");
            }
            return Ok(comment);
        }

        // API UpdateComment
        [HttpPut("{cid}")]
        public async Task<IActionResult> UpdateComment(int cid, [FromBody] CommentRequest request)
        {
            if (request == null)
            {
                return BadRequest("Dữ liệu không hợp lệ");
            }

            var updatedComment = await _commentService.UpdateCommentAsync(cid, request);
            if (updatedComment == null)
            {
                return NotFound($"Comment với CID {cid} không tồn tại.");
            }

            return Ok(updatedComment);
        }

    }
}
