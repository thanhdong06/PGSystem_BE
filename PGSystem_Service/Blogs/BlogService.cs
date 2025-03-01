using AutoMapper;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Repository.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }
        public async Task<BlogResponse> CreateBlogAsync(BlogRequest request)
        {
            var entity = _mapper.Map<Blog>(request);
            entity.Title = request.Title;
            entity.Content = request.Content;
            entity.CreateAt = entity.UpdateAt = DateTime.UtcNow;

            var createdBlog = await _blogRepository.CreateBlogsAsync(entity);

            return _mapper.Map<BlogResponse>(createdBlog);
        }

        public async Task<bool> DeleteBlogsAsync(int bid)
        {
            return await _blogRepository.DeleteBlogs(bid);
        }

        public async Task<IEnumerable<BlogResponse>> GetAllBlogByAID(int aid)
        {
            var blog = await _blogRepository.GetBlogByAidAsync(aid);
            return _mapper.Map<IEnumerable<BlogResponse>>(blog);
        }

        public async Task<List<BlogResponse>> GetAllBlogsAsync()
        {
            var blog = await _blogRepository.GetAllBlogAsync();

            return blog.Select(b => new BlogResponse
            {
                Title = b.Title,
                Content = b.Content,
            }).ToList();
        }
        public async Task<BlogResponse?> GetBlogByBIDAsync(int bid)
        {
            var blog = await _blogRepository.GetByIdAsync(bid);
            if (blog == null)
            {
                return null;
            }
            return _mapper.Map<BlogResponse>(blog);
        }
        public async Task<BlogResponse?> UpdateBlogAsync(int bid, BlogRequest request)
        {
            var blog = await _blogRepository.GetByIdAsync(bid);
            if (blog == null)
            {
                return null; // Trả về null nếu không tìm thấy Blog
            }

            // Cập nhật thông tin
            blog.Title = request.Title;
            blog.Content = request.Content;
            blog.Type = request.Type;
            blog.UpdateAt = DateTime.UtcNow;

            await _blogRepository.UpdateAsync(blog);
            await _blogRepository.SaveChangesAsync();

            return _mapper.Map<BlogResponse>(blog);
        }
    }
}
