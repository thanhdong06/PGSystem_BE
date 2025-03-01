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
        Task<List<Blog>> GetAllBlogAsync();
        Task<IEnumerable<Blog>> GetBlogByAidAsync(int aid);
        Task<Blog> CreateBlogsAsync(Blog entity);
        Task<bool> DeleteBlogs(int bid);
        Task<Blog?> GetByIdAsync(int bid);
        Task UpdateAsync(Blog blog);
        Task SaveChangesAsync();
    }
}
