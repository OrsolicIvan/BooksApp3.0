using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int, 
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, 
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasOne(u => u.Renter)
                .WithMany(ap => ap.RentedBooks)
                .HasForeignKey(oi => oi.RenterId)
                .OnDelete(DeleteBehavior.ClientCascade);
            
            modelBuilder.Entity<AppUser>()
                .HasMany(ap => ap.OwnedBooks)
                .WithOne(u => u.Owner)
                .HasForeignKey(oi => oi.OwnerId)
                .OnDelete(DeleteBehavior.ClientCascade);
            
            modelBuilder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            modelBuilder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            modelBuilder.Entity<Book_Author>()
                .HasKey(k => new { k.BookId, k.AuthorId });
                


            modelBuilder.Entity<Book_Author>()
                .HasOne(a => a.Author)
                .WithMany(b => b.Book_Authors)
                .HasForeignKey(ai => ai.AuthorId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Book_Author>()
                .HasOne(a => a.Book)
                .WithMany(b => b.Book_Authors)
                .HasForeignKey(ai => ai.BookId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Image>()
               .HasOne(u => u.Book)
               .WithOne(p => p.Images)
               .HasForeignKey<Image>(ui => ui.BookId);





        }
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Book_Author> Books_Authors { get; set; }
        public DbSet<Image> Images { get; set; }


    }
}
