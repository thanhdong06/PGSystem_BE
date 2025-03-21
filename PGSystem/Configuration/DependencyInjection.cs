using Microsoft.EntityFrameworkCore;
using PGSystem_DataAccessLayer.DBContext;
using PGSystem_Repository.Users;
using PGSystem_Service.Users;
using PGSystem_DataAccessLayer.MappingAndPaging;
using AutoMapper;
using PGSystem_DataAccessLayer.Password;
using PGSystem_Repository.Admin;
using PGSystem_Service.Admin;
using PGSystem_Repository.GrowthRecords;
using PGSystem_Service.GrowthRecords;
using PGSystem_Repository.PregnancyRecords;
using PGSystem_Service.PregnancyRecords;
using PGSystem_Repository.Reminders;
using PGSystem_Service.Reminders;
using PGSystem_Repository.Blogs;
using PGSystem_Repository.Comments;
using PGSystem_Service.Blogs;
using PGSystem_Service.Comments;
using PGSystem_Repository.Members;
using PGSystem_Service.Members;
using PGSystem_Repository.MembershipRepository;
using PGSystem_Service.Memberships;
using PGSystem_Repository.TransactionRepository;

namespace PGSystem.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IGrowthRecordRepository, GrowthRecordRepository>();
            services.AddScoped<IGrowthRecordService, GrowthRecordService>();
            services.AddScoped<IPregnancyRecordRepository, PregnancyRecordRepository>();
            services.AddScoped<IPregnancyRecordService, PregnancyRecordService>();
            services.AddScoped<IReminderRepository, ReminderRepository>();
            services.AddScoped<IReminderService, ReminderService>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IMembersRepository, MembersRepository>();
            services.AddScoped<IMembersService, MembersService>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            return services;
        }
    }
}
