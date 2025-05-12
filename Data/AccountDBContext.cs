using Homework_SkillTree.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace Homework_SkillTree.Data
{
    public partial class AccountDBContext:DbContext
    {
        public AccountDBContext(DbContextOptions<AccountDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<JoinActBook> JoinActBooks { get; set; } = null!;
        public virtual DbSet<AccountBook> AccountBooks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JoinActBook>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__AccountB__3214EC07D1A2F3C8");
            });

            modelBuilder.Entity<AccountBook>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__AccountB__3214EC07D1A2F3C8");
            });
        }
    }
}
