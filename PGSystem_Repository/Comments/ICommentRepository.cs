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
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<bool> DeleteComment(int cid);
        Task<IEnumerable<Comment>> GetAllByBIDAsync(int bid);
        Task<Comment?> GetByIdAsync(int cid);
        Task UpdateAsync(Comment comment);
        Task SaveChangesAsync();
    }
}
