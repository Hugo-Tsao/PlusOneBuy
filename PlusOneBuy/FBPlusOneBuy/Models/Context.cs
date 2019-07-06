namespace FBPlusOneBuy.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<FanPage> FanPages { get; set; }
        public virtual DbSet<LivePost> LivePosts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FanPage>()
                .HasMany(e => e.LivePosts)
                .WithRequired(e => e.FanPage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LivePost>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.LivePost)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductImage)
                .IsFixedLength();

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);
        }
    }
}
