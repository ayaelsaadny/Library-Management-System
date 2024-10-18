using book.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace book.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    { 

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Buy> buys { get; set; }
        public DbSet<Book> books { get; set; }
        public DbSet<Borrow> Borrows { get; set; }    
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
             
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "admin",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                 new IdentityRole()
                 {
                     Id = Guid.NewGuid().ToString(),
                     Name = "User",
                     NormalizedName = "user",
                     ConcurrencyStamp = Guid.NewGuid().ToString(),
                 }
                 );

            // Configure the many-to-many relationship
            builder.Entity<Buy>()
                .HasKey(ub => new { ub.UserId, ub.BookId }); // Composite primary key

            builder.Entity<Buy>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.UserBooks)
                .HasForeignKey(ub => ub.UserId);

            builder.Entity<Buy>()
                .HasOne(ub => ub.Book)
                .WithMany(b => b.UserBooks)
                .HasForeignKey(ub => ub.BookId);

          

            base.OnModelCreating(builder);

        }
    }
}
