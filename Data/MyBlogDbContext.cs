using Microsoft.EntityFrameworkCore;
using Homework_SkillTree.Models;

public class MyBlogDbContext : DbContext
{
    public MyBlogDbContext(DbContextOptions<MyBlogDbContext> options) : base(options) { }

    // 定義資料表
    public DbSet<IncomeTableViewModel> IncomeTable { get; set; }
}
