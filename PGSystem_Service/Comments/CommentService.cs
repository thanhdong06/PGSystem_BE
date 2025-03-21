using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Repository.Blogs;
using PGSystem_Repository.Comments;
using PGSystem_Repository.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Comments
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;
        private readonly IMembersRepository _membersRepository;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IBlogRepository blogRepository, IMembersRepository membersRepository)
        {
            _commentRepository = commentRepository;
            _blogRepository = blogRepository;
            _mapper = mapper;
            _membersRepository = membersRepository;
        }

        public async Task<CommentResponse> CreateCommentAsync(CommentRequest request, ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User is not authenticated");

            if (!int.TryParse(userId, out int userIdd))
                throw new Exception("Invalid user ID format");

            var userRole = user.FindFirst(ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(userRole) || userRole != "Member")
                throw new UnauthorizedAccessException("Only members can comment");

            var blog = await _blogRepository.GetByIdAsync(request.BID);
            if (blog == null) throw new Exception("Blog does not exist");

            var member = await _membersRepository.GetMemberByIdAsync(userIdd);
            if (member == null) throw new Exception("Member does not exist!");

            var comment = new Comment
            {
                Content = request.Content,
                BID = request.BID,
                MemberID = member.MemberID,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                IsDeleted = false
            };

            var createdComment = await _commentRepository.CreateCommentAsync(comment);

            return new CommentResponse
            {
                CID = createdComment.CID,
                Content = createdComment.Content,
                BID = createdComment.BID,
                MemberID = createdComment.MemberID,
                CreateAt = createdComment.CreateAt
            };
        }


        public async Task<bool> DeleteCommentAsync(int cid)
        {
            return await _commentRepository.DeleteComment(cid);
        }

        public async Task<IEnumerable<CommentResponse>> GetAllCommentsAsync()
        {
            var comments = await _commentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CommentResponse>>(comments);
        }

        public async Task<IEnumerable<CommentResponse>> GetAllCommentsByBIDAsync(int bid)
        {
            var comments = await _commentRepository.GetAllByBIDAsync(bid);
            return _mapper.Map<IEnumerable<CommentResponse>>(comments);
        }
        public async Task<CommentResponse?> GetCommentByCIDAsync(int cid)
        {
            var comment = await _commentRepository.GetByIdAsync(cid);
            if (comment == null)
            {
                return null;
            }
            return _mapper.Map<CommentResponse>(comment);
        }

        public async Task<CommentResponse?> UpdateCommentAsync(int cid, CommentUpdateRequest request)
        {
            var comment = await _commentRepository.GetByIdAsync(cid);
            if (comment == null)
            {
                return null;
            }

            // Cập nhật nội dung comment
            comment.Content = request.Content;
            comment.UpdateAt = DateTime.UtcNow;

            await _commentRepository.UpdateAsync(comment);
            await _commentRepository.SaveChangesAsync();

            return _mapper.Map<CommentResponse>(comment);
        }
    }
}
