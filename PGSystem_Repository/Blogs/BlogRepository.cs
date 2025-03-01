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
        public async Task<Blog> CreateBlogsAsync(Blog entity)
        {
            _context.Blogs.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteBlogs(int bid)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.BID == bid && !b.IsDeleted);

            if (blog == null)
            {
                return false;
            }
            blog.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Blog>> GetAllBlogAsync()
        {
            {
                return await _context.Blogs.ToListAsync();
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
                .Include(b => b.Member)
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
    }
}
