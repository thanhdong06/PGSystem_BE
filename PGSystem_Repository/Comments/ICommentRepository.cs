using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.Comments
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentAsync();
        Task<Comment> CreateCommentsAsync(Comment entity);
        Task<bool> DeleteComment(int cid);
        Task<IEnumerable<Comment>> GetAllCommentByBID(int bid);
        Task<Comment?> GetByIdAsync(int cid);
        Task UpdateAsync(Comment comment);
        Task SaveChangesAsync();
    }
}
