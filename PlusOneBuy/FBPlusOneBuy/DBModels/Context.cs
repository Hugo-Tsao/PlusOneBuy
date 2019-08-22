namespace FBPlusOneBuy.DBModels
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

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Campaign> Campaigns { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<FanPage> FanPages { get; set; }
        public virtual DbSet<GroupOrder> GroupOrders { get; set; }
        public virtual DbSet<GroupOrderDetail> GroupOrderDetails { get; set; }
        public virtual DbSet<LineCustomer> LineCustomers { get; set; }
        public virtual DbSet<LineGroup> LineGroups { get; set; }
        public virtual DbSet<LivePost> LivePosts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<StoreManager> StoreManagers { get; set; }
        public virtual DbSet<Viewer> Viewers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Campaign>()
                .HasMany(e => e.GroupOrders)
                .WithRequired(e => e.Campaign)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerID)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.SalesOrders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FanPage>()
                .HasMany(e => e.LivePosts)
                .WithRequired(e => e.FanPage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupOrder>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<GroupOrder>()
                .HasMany(e => e.GroupOrderDetails)
                .WithRequired(e => e.GroupOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupOrderDetail>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LineCustomer>()
                .HasMany(e => e.GroupOrderDetails)
                .WithRequired(e => e.LineCustomer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LineGroup>()
                .HasMany(e => e.Campaigns)
                .WithRequired(e => e.LineGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LivePost>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LivePost>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.LivePost)
                .HasForeignKey(e => e.LiveID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LivePost>()
                .HasMany(e => e.SalesOrders)
                .WithRequired(e => e.LivePost)
                .HasForeignKey(e => e.LiveID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LivePost>()
                .HasMany(e => e.Viewers)
                .WithOptional(e => e.LivePost)
                .HasForeignKey(e => e.LiveID);

            modelBuilder.Entity<Order>()
                .Property(e => e.CustomerID)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductImage)
                .IsFixedLength();

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Campaigns)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.SalesOrders)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SalesOrder>()
                .Property(e => e.CustomerID)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrder>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);
        }
    }
}
