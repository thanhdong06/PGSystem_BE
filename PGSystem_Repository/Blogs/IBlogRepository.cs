using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Repository.Blogs
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> GetAllBlogAsync();
        Task<IEnumerable<Blog>> GetBlogByAidAsync(int aid);
        Task<List<Blog>> GetBlogsByUserIdAsync(string userId);
        Task<Blog> CreateBlogsAsync(Blog blog);
        Task DeleteBlogsByUserIdAsync(string userId);
        
        Task<Blog?> GetByIdAsync(int bid);
        Task UpdateAsync(Blog blog);
        Task SaveChangesAsync();
    }
}
