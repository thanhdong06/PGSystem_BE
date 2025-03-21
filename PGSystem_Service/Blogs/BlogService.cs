using AutoMapper;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.Entities;
using PGSystem_Repository.Blogs;
using PGSystem_Repository.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_Service.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        private readonly IMembersRepository _membersRepository;

        public BlogService(IBlogRepository blogRepository, IMapper mapper, IMembersRepository membersRepository)
        {
            _membersRepository = membersRepository;
            _blogRepository = blogRepository;
            _mapper = mapper;
        }
        public async Task<BlogResponse> CreateBlogAsync(BlogRequest request, ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User is not authenticated");

            var member = await _membersRepository.GetMemberByIdAsync(userId);
            if (member == null)
                throw new Exception("Member does not exist");

            var userRole = user.FindFirst(ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(userRole) || userRole != "Member")
                throw new UnauthorizedAccessException("Only members can create blogs");

            var newBlog = new Blog
            {
                Title = request.Title,
                Content = request.Content,
                Type = request.Type,
                Member = member,
                AID = member.MemberID,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                IsDeleted = false
            };

            var createdBlog = await _blogRepository.CreateBlogsAsync(newBlog);

            return new BlogResponse
            {
                BID = createdBlog.BID,
                Title = createdBlog.Title,
                Content = createdBlog.Content,
                Type = createdBlog.Type,
                CreateAt = createdBlog.CreateAt,
                UpdateAt = createdBlog.UpdateAt,
                AID = createdBlog.AID
            };
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

        public async Task<IEnumerable<BlogResponse>> GetAllBlogsAsync()
        {
            var blogs = await _blogRepository.GetAllBlogAsync();
            return _mapper.Map<IEnumerable<BlogResponse>>(blogs);
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
        public async Task<BlogResponse?> UpdateBlogAsync(int bid, BlogUpdateRequest  request)
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
