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

        public async Task<CommentResponse> CreateCommentAsync(CommentRequest request)
        {
            // Kiểm tra Blog có tồn tại không
            var blog = await _blogRepository.GetByIdAsync(request.BID);
            if (blog == null) throw new Exception("Blog is not exist");

            // Kiểm tra Member có tồn tại không
            var member = await _membersRepository.GetMemberByIdAsync(request.MemberID);
            if (member == null) throw new Exception("Member is not exist!");

            var comment = new Comment
            {
                Content = request.Content,
                BID = request.BID,
                MemberID = request.MemberID,
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
