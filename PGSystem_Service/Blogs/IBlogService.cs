using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Blogs
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogResponse>> GetAllBlogsAsync();
        Task<BlogResponse> CreateBlogAsync(BlogRequest request);
        Task<bool> DeleteBlogsAsync(int bid);
        Task<IEnumerable<BlogResponse>> GetAllBlogByAID(int aid);
        Task<BlogResponse?> GetBlogByBIDAsync(int bid);
        Task<BlogResponse?> UpdateBlogAsync(int bid, BlogUpdateRequest request);
    }
}
