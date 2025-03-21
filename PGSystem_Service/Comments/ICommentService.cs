using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Comments
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentResponse>> GetAllCommentsAsync();

        Task<CommentResponse> CreateCommentAsync(CommentRequest request, ClaimsPrincipal user);
        Task<bool> DeleteCommentAsync(int cid);
        Task<IEnumerable<CommentResponse>> GetAllCommentsByBIDAsync(int bid);
        Task<CommentResponse?> GetCommentByCIDAsync(int cid);
        Task<CommentResponse?> UpdateCommentAsync(int cid, CommentUpdateRequest request);
    }
}
