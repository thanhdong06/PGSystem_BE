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
            if (comments == null || !comments.Any())
            {
                return NotFound(new JsonResponse<List<CommentResponse>>(new List<CommentResponse>(), StatusCodes.Status404NotFound, "No Comment"));
            }
            return Ok(comments);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateComment([FromBody] CommentRequest request)
        {
            var memberId = User.FindFirst("MemberId")?.Value;
            if (string.IsNullOrEmpty(memberId))
            {
                return Unauthorized(new JsonResponse<string>("Unauthorized: MemberId not found in token", 401, null));
            }

            request.MemberID = int.Parse(memberId);

            if (request == null)
            {
                return BadRequest("Invalid Data");
            }

            var createdComment = await _commentService.CreateCommentAsync(request, User);
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
        [HttpGet("by-bid/{bid}")]
        public async Task<IActionResult> GetAllCommentsByBID(int bid)
        {
            var comments = await _commentService.GetAllCommentsByBIDAsync(bid);
            if (!comments.Any())
            {
                return NotFound("There are no comments for this blog.");
            }
            return Ok(comments);
        }
        [HttpGet("{cid}")]
        public async Task<IActionResult> GetCommentByCID(int cid)
        {
            var comment = await _commentService.GetCommentByCIDAsync(cid);
            if (comment == null)
            {
                return NotFound($"Comment with CID {cid} does not exist.");
            }
            return Ok(comment);
        }

        // API UpdateComment
        [HttpPut("{cid}")]
        public async Task<IActionResult> UpdateComment(int cid, [FromBody] CommentUpdateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid Data");
            }

            var updatedComment = await _commentService.UpdateCommentAsync(cid, request);
            if (updatedComment == null)
            {
                return NotFound($"Comment with CID {cid} does not exist.");
            }

            return Ok(updatedComment);
        }

    }
}
