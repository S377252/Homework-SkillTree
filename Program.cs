using Homework_SkillTree.Data;
using Homework_SkillTree.Service;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);


// Ū���s�u�r��
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ���U DbContext
builder.Services.AddDbContext<AccountDBContext>(options =>
    options.UseSqlServer(connectionString));

//(�`�J)�[�JAccountService
builder.Services.AddScoped<IAccountService, AccountService>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=JoinActBooks}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
