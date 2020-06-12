using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using NotesApi.Models;
namespace NotesApi.Database.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TaskNote> TaskNotes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(x => x.Id);
            builder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Entity<User>().Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(x => x.Lastname).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(x => x.Login).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(x => x.Password).IsRequired();
            builder.Entity<User>().Property(x => x.Info).IsRequired();
            builder.Entity<User>().HasAlternateKey(x => x.Login);
            builder.Entity<User>().HasMany(x => x.UserRoles).WithOne(x => x.User);
            builder.Entity<User>().HasMany(p => p.TaskNotes).WithOne(p => p.User).HasForeignKey(p => p.Id);


            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<Role>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Role>().Property(x => x.Name).IsRequired();
            builder.Entity<Role>().HasAlternateKey(x => x.Name);
            builder.Entity<Role>().HasMany(x => x.UserRoles).WithOne(x => x.Role);


            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<TaskNote>().ToTable("TaskNotes");
            builder.Entity<TaskNote>().HasKey(x => x.Id);
            builder.Entity<TaskNote>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<TaskNote>().Property(x => x.Header).IsRequired().HasMaxLength(30);
            builder.Entity<TaskNote>().Property(x => x.Description).IsRequired().HasMaxLength(255);
            builder.Entity<TaskNote>().Property(x => x.Priority).IsRequired();
            builder.Entity<TaskNote>().Property(x => x.CreationTime).IsRequired();

            User u1 = new User
            {
                Id = 1,
                Name = "James",
                Lastname = "Bond",
                Login = "jb007",
                Password = "spy",
                Info = "I am spy"
            };
            User u2 = new User
            {
                Id = 2,
                Name = "James",
                Lastname = "Bond",
                Login = "jb00722",
                Password = "spy",
                Info = "I am spy"
            };
            User u3 = new User
            {
                Id = 3,
                Name = "James",
                Lastname = "Bond",
                Login = "jb007222",
                Password = "spy",
                Info = "I am spy"
            };
            User u4 = new User
            {
                Id = 4,
                Name = "James",
                Lastname = "Bond",
                Login = "jb0071121",
                Password = "spy",
                Info = "I am spy"
            };
            User u5 = new User
            {
                Id = 5,
                Name = "James",
                Lastname = "Bond",
                Login = "jb0071111",
                Password = "spy",
                Info = "I am spy"
            };
            User u6 = new User
            {
                Id = 6,
                Name = "James",
                Lastname = "Bond",
                Login = "jb007111",
                Password = "spy",
                Info = "I am spy"
            };
            User u7 = new User
            {
                Id = 7,
                Name = "James",
                Lastname = "Bond",
                Login = "jb00711",
                Password = "spy",
                Info = "I am spy"
            };
            User u8 = new User
            {
                Id = 8,
                Name = "James",
                Lastname = "Bond",
                Login = "jb0071",
                Password = "spy",
                Info = "I am spy"
            };
            builder.Entity<User>().HasData(u1, u2, u3, u4, u5, u6, u7, u8);

            Role sa = new Role { Id = 1, Name = "SuperAdmin" };
            Role admin = new Role { Id = 2, Name = "Admin" };
            Role user = new Role { Id = 3, Name = "User" };

            UserRole ur1 = new UserRole { UserId = 1, RoleId = 1 };
            UserRole ur2 = new UserRole { UserId = 1, RoleId = 2 };


        }
    }
}

