using Microsoft.EntityFrameworkCore;
using System;
using WishList.Entities;

namespace WishList.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WishLst>()
                .HasKey(wl => new { wl.UserId, wl.ProductId });
            modelBuilder.Entity<WishLst>()
                .HasOne(wl => wl.User)
                .WithMany(u => u.Wishes)
                .HasForeignKey(wl => wl.UserId);
            modelBuilder.Entity<WishLst>()
                .HasOne(wl => wl.Product)
                .WithMany(u => u.Wishes)
                .HasForeignKey(wl => wl.ProductId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<WishLst> Wishes { get; set; }
    }
}