using Microsoft.EntityFrameworkCore;
using Homework_SkillTree.Models;

public class MyBlogDbContext : DbContext
{
    public MyBlogDbContext(DbContextOptions<MyBlogDbContext> options) : base(options) { }

    // �w�q��ƪ�
    public DbSet<IncomeTableViewModel> IncomeTable { get; set; }
}
