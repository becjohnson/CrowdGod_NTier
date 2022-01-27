using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrowdGod.Data
{
    public class CrowdGodDbContext : IdentityDbContext<ApplicationUser>
    {
        public CrowdGodDbContext(DbContextOptions<CrowdGodDbContext> options)
            : base(options)
        { }
        public static CrowdGodDbContext Create(DbContextOptions<CrowdGodDbContext> options)
        {
            return new CrowdGodDbContext(options);
        }

        public DbSet<Question>? Questions { get; set; }
        public DbSet<Answer>? Answers { get; set; }
        public DbSet<Reply>? Replies { get; set; }
        public DbSet<Tag>? Tags { get; set; }
        public DbSet<QuestionTag>? QuestionTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Question>()
                .HasMany(a => a.Answers)
                .WithOne(a => a.Question);

            builder.Entity<Answer>()
                .HasMany(a => a.Replies)
                .WithOne(a => a.Answer);

            builder.Entity<Question>()
                .HasMany(p => p.Tags)
                .WithMany(p => p.Questions)
                .UsingEntity<QuestionTag>(
            j => j.HasOne(t => t.Tag).WithMany(p => p.QuestionTags),
            j => j.HasOne(t => t.Question).WithMany(p => p.QuestionTags));

            base.OnModelCreating(builder);
        }
    }
}

