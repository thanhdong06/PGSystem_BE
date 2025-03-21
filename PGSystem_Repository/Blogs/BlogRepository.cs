using Microsoft.EntityFrameworkCore;
using PGSystem_DataAccessLayer.DBContext;
using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.Blogs
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDBContext _context;

        public BlogRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Blog>> GetBlogsByUserIdAsync(string userId)
        {
            return await _context.Blogs
                .Where(b => b.Member.UserUID.Equals(userId) && !b.IsDeleted) 
                .Include(b => b.Comments) 
                .ToListAsync(); 
        }

        public async Task DeleteBlogsByUserIdAsync(string userId)
        {
            var blogs = await _context.Blogs
                .Where(b => b.Member.UserUID.Equals(userId) && !b.IsDeleted) // Chỉ lấy blog chưa bị xóa
                .Include(b => b.Comments)
                .ToListAsync();

            if (!blogs.Any()) return;

            // Cập nhật trạng thái IsDeleted của Blog và Comment
            foreach (var blog in blogs)
            {
                blog.IsDeleted = true;
                foreach (var comment in blog.Comments)
                {
                    comment.IsDeleted = true;
                }
            }
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Blog>> GetAllBlogAsync()
        {
            {
                return await _context.Blogs
                    .Include(b => b.Member).ThenInclude(m=>m.User)
            .Where(b => !b.IsDeleted)
            .ToListAsync();
            }
        }

        public async Task<IEnumerable<Blog>> GetBlogByAidAsync(int aid)
        {
            return await _context.Blogs
            .Where(b => b.AID == aid && !b.IsDeleted)
            .ToListAsync();
        }
        public async Task<Blog?> GetByIdAsync(int bid)
        {
            return await _context.Blogs
                .Include(b => b.Member).ThenInclude(m=> m.User)
                .FirstOrDefaultAsync(b => b.BID == bid && !b.IsDeleted);
        }
        public async Task UpdateAsync(Blog blog)
        {
            _context.Blogs.Update(blog);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Blog> CreateBlogsAsync(Blog blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog;
        }
    }
}
