using Bookish.Models;
using Bookish.Models.Book;
using Bookish.Models.Member;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

namespace Bookish
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Book { get; set; }
        public DbSet<Copy> Copy { get; set; }
        public DbSet<Loan> Loan { get; set; }
        public DbSet<Member> Member { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=LibraryDB;User Id=sa;Password=Is0belNa0mi;Encrypt=False;");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Copy>()
                .HasKey(l => new { l.BookId, l.CopyId });
            
            modelBuilder.Entity<Loan>()
                .HasKey(l => new { l.CopyId, l.MemberId, l.DateCheckedOut });
        }
    }
}