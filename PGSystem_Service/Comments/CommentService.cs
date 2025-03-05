using AutoMapper;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Repository.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Comments
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<CommentResponse> CreateCommentAsync(CommentRequest request)
        {
            var newComment = new Comment
            {
                Content = request.Content,
                BID = request.BID,
                MemberID = request.MemberID,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _commentRepository.CreateAsync(newComment);
            await _commentRepository.SaveChangesAsync();

            return _mapper.Map<CommentResponse>(newComment);
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

        public async Task<IEnumerable<CommentResponse>> GetAllCommentByBID(int bid)
        {
            var comments = _commentRepository.GetAllCommentByBID(bid);
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

        public async Task<CommentResponse?> UpdateCommentAsync(int cid, CommentRequest request)
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
