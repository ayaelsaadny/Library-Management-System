using book.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace book.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    { 

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
   
        public DbSet<Book> books { get; set; }
        public DbSet<Buy> Buys { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
             
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            // Configure the many-to-many relationship
            modelBuilder.Entity<Buy>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Buy>()
                .HasOne(b => b.User)
                .WithMany(u => u.Buys)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Buy>()
                .HasOne(b => b.Book)
                .WithMany(bk => bk.Buys)
                .HasForeignKey(b => b.BookId);
            ////////////borrow////////////
            modelBuilder.Entity<Borrow>()
           .HasKey(b => b.Id);

            modelBuilder.Entity<Borrow>()
                .HasOne(b => b.User)
                .WithMany(u => u.Borrows)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Borrow>()
                .HasOne(b => b.Book)
                .WithMany(bk => bk.Borrows)
                .HasForeignKey(b => b.BookId);

            /////////user-admin///////////
            modelBuilder.Entity<IdentityRole>().HasData(
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
            base.OnModelCreating(modelBuilder);

        }
    }
}
