using Microsoft.EntityFrameworkCore;
using PGSystem_DataAccessLayer.DBContext;
using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.Comments
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDBContext _context;

        public CommentRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> DeleteComment(int cid)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.CID == cid && !c.IsDeleted);

            if (comment == null)
            {
                return false;
            }
            comment.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _context.Comments
                .Include(c=> c.Member)
                .ThenInclude(m=>m.User)
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllByBIDAsync(int bid)
        {
            return await _context.Comments
                .Include(c=> c.Member).ThenInclude(m=>m.User)
                .Where(c => c.BID == bid && !c.IsDeleted)
                .ToListAsync();
        }
        public async Task<Comment?> GetByIdAsync(int cid)
        {
            return await _context.Comments
                .Include(c => c.Member).ThenInclude(m => m.User) // Nếu cần lấy thông tin Member
                .Include(c => c.Blog)   // Nếu cần lấy thông tin Blog
                .FirstOrDefaultAsync(c => c.CID == cid && !c.IsDeleted);
        }

        public async Task UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
