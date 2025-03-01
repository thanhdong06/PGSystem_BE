using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Comments
{
    public interface ICommentService
    {
        Task<List<CommentResponse>> GetAllCommentAsync();

        Task<CommentResponse> CreateCommentAsync(CommentRequest request);
        Task<bool> DeleteCommentAsync(int cid);
        Task<IEnumerable<CommentResponse>> GetAllCommentByBID(int bid);
        Task<CommentResponse?> GetCommentByCIDAsync(int cid);
        Task<CommentResponse?> UpdateCommentAsync(int cid, CommentRequest request);
    }
}
