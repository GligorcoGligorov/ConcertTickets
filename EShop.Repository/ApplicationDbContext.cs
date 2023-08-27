using EShop.Domain.DomainModels;
using EShop.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace EShop.Repository
{
    public class ApplicationDbContext : IdentityDbContext<EShopApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Concert> Concerts { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<ConcertInShoppingCart> ConcertInShoppingCarts { get; set; }
        public virtual DbSet<ConcertInOrder> ConcertInOrders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Concert>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ShoppingCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            //builder.Entity<ConcertInShoppingCart>()
            //    .HasKey(z => new { z.ConcertId, z.ShoppingCartId });

            builder.Entity<ConcertInShoppingCart>()
                .HasOne(z => z.Concert)
                .WithMany(z => z.ConcertInShoppingCarts)
                .HasForeignKey(z => z.ShoppingCartId);

            builder.Entity<ConcertInShoppingCart>()
                .HasOne(z => z.ShoppingCart)
                .WithMany(z => z.ConcertInShoppingCarts)
                .HasForeignKey(z => z.ConcertId);

            builder.Entity<ShoppingCart>()
                .HasOne<EShopApplicationUser>(z => z.Owner)
                .WithOne(z => z.UserCart)
                .HasForeignKey<ShoppingCart>(z => z.OwnerId);

        //    builder.Entity<ConcertInOrder>()
        //        .HasKey(z => new { z.ConcertId, z.OrderId });

            builder.Entity<ConcertInOrder>()
                .HasOne(z => z.OrderedConcert)
                .WithMany(z => z.ConcertInOrders)
                .HasForeignKey(z => z.OrderId);

            builder.Entity<ConcertInOrder>()
                .HasOne(z => z.UserOrder)
                .WithMany(z => z.ConcertInOrders)
                .HasForeignKey(z => z.ConcertId);

        }
    }
}
