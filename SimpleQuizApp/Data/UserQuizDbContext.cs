using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleQuizApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SimpleQuizApp.Data
{
    public class UserQuizDbContext : IdentityDbContext<User>
    {
        public UserQuizDbContext(DbContextOptions<UserQuizDbContext> options) : base(options)
        {
        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional model configuration can go here if needed
        }

    }
}
