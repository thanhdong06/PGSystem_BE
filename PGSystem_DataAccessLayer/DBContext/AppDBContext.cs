using Microsoft.EntityFrameworkCore;
using PGSystem_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DBContext
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() { }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<GrowthRecord> GrowthRecords { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<PregnancyGrowthReport> PregnancyGrowthReports { get; set; }
        public DbSet<PregnancyRecord> PregnancyRecords { get; set; }
        public DbSet<RStatus> RStatuses { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
            new User
            {
                UID = 1,            
                Email = "admin@gmail.com",
                Password = "12345",
                Phone = "0123456789",
                Role = "Admin",
                CreateAt = DateTime.Today,
                UpdateAt = DateTime.Now,
            }
            );
            ConfigureModel(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-NLTHM945\\DIVAORAM;uid=sa;pwd=12345;Database=SWP391PGSystem;TrustServerCertificate=True;");
            }
        }
        private void ConfigureModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
            .HasKey(a => a.AdminID);
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.User)
                .WithMany(u => u.Admins)
                .HasForeignKey(a => a.AdminID);

            // Configure Alert
            modelBuilder.Entity<Alert>()
                .HasKey(a => a.AlertID);
            modelBuilder.Entity<Alert>()
                .HasOne(a => a.PregnancyRecord)
                .WithMany(p => p.Alerts)
                .HasForeignKey(a => a.PID);

            // Configure Blog
            modelBuilder.Entity<Blog>()
                .HasKey(b => b.BID);
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Member)
                .WithMany(m => m.Blogs)
                .HasForeignKey(b => b.AID);

            // Configure Comment
            modelBuilder.Entity<Comment>()
                .HasKey(c => c.CID);
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Blog)
                .WithMany()
                .HasForeignKey(c => c.BID)
                .OnDelete(DeleteBehavior.Restrict);


            // Configure GrowthRecord
            modelBuilder.Entity<GrowthRecord>()
                .HasKey(g => g.GID);
            modelBuilder.Entity<GrowthRecord>()
                .HasOne(g => g.PregnancyRecord)
                .WithMany(p => p.GrowthRecords)
                .HasForeignKey(g => g.PID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<GrowthRecord>()
                .Property(g => g.Height)
                .HasColumnType("decimal(18, 4)");
            modelBuilder.Entity<GrowthRecord>()
                .Property(g => g.Weight)
                .HasColumnType("decimal(18, 4)");

            // Configure Member
            modelBuilder.Entity<Member>()
                .HasKey(m => m.MemberID);
            modelBuilder.Entity<Member>()
                .HasOne(m => m.Membership)
                .WithMany(ms => ms.Members)
                .HasForeignKey(m => m.MembershipID);
            modelBuilder.Entity<Member>()
                .HasOne(m => m.PregnancyRecord)
                .WithOne(p => p.Member)
                .HasForeignKey<PregnancyRecord>(p => p.MemberMemberID)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Membership>()
                .Property(ms => ms.Price)  
                .HasColumnType("decimal(18, 2)");

            // Configure Membership
            modelBuilder.Entity<Membership>()
                .HasKey(ms => ms.MID);

            // Configure PregnancyGrowthReport
            modelBuilder.Entity<PregnancyGrowthReport>()
                .HasKey(pg => pg.PGID);
            modelBuilder.Entity<PregnancyGrowthReport>()
                .HasOne(pg => pg.PregnancyRecord)
                .WithMany(p => p.PregnancyGrowthReports)
                .HasForeignKey(pg => pg.PID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PregnancyGrowthReport>()
                .Property(pg => pg.Height)
                .HasColumnType("decimal(18, 4)");
            modelBuilder.Entity<PregnancyGrowthReport>()
                .Property(pg => pg.Weight)
                .HasColumnType("decimal(18, 4)");

            // Configure PregnancyRecord
            modelBuilder.Entity<PregnancyRecord>()
                .HasKey(p => p.PID);

            // Configure R-Status
            modelBuilder.Entity<RStatus>()
                .HasKey(r => r.SID);

            // Configure Reminder
            modelBuilder.Entity<Reminder>()
                .HasKey(r => r.RID);
            modelBuilder.Entity<Reminder>()
                .HasOne(r => r.RStatus)
                .WithMany(rs => rs.Reminders)
                .HasForeignKey(r => r.SID)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Reminder>()
                .HasOne(r => r.Member)
                .WithMany(m => m.Reminders)
                .HasForeignKey(r => r.MemberID)
                .OnDelete(DeleteBehavior.Restrict);


            // Configure User
            modelBuilder.Entity<User>()
                .HasKey(u => u.UID);
        }
    }
}
