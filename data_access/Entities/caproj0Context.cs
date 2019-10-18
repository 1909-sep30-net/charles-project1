using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace data_access.Entities
{
    public partial class caproj0Context : DbContext
    {
        public caproj0Context()
        {
        }

        public caproj0Context(DbContextOptions<caproj0Context> options)
            : base(options)
        {
        }

        public virtual DbSet<CustOrder> CustOrder { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<LineItem> LineItem { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<StoreLocation> StoreLocation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:allensworthc-sqlserver.database.windows.net,1433;Initial Catalog=caproj0;Persist Security Info=False;User ID=allensworthc;Password=GigaPlex8*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__CustOrde__C3905BAF8D5BD29A");

                entity.ToTable("CustOrder", "caproj0");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustOrder)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustOrder__Custo__00200768");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.CustOrder)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustOrder__Locat__01142BA1");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "caproj0");

                entity.HasIndex(e => e.Phone)
                    .HasName("UQ__Customer__5C7E359E0FB4B7DA")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerPw)
                    .IsRequired()
                    .HasColumnName("CustomerPW")
                    .HasMaxLength(32);

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("FName")
                    .HasMaxLength(32);

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasColumnName("LName")
                    .HasMaxLength(32);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => new { e.LocationId, e.ProductId })
                    .HasName("PK_LocProdID");

                entity.ToTable("Inventory", "caproj0");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Locat__76969D2E");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Produ__778AC167");
            });

            modelBuilder.Entity<LineItem>(entity =>
            {
                entity.ToTable("LineItem", "caproj0");

                entity.Property(e => e.LineItemId).HasColumnName("LineItemID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.LineItem)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LineItem__OrderI__07C12930");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.LineItem)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LineItem__Produc__08B54D69");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.ToTable("Manager", "caproj0");

                entity.Property(e => e.ManagerId).HasColumnName("ManagerID");

                entity.Property(e => e.ManagerPw)
                    .IsRequired()
                    .HasColumnName("ManagerPW")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "caproj0");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.Pname)
                    .IsRequired()
                    .HasColumnName("PName")
                    .HasMaxLength(15);

                entity.Property(e => e.SalesName).HasMaxLength(40);

                entity.Property(e => e.SalesPrice)
                    .HasColumnType("numeric(23, 6)")
                    .HasComputedColumnSql("([Cost]*(2.00))");
            });

            modelBuilder.Entity<StoreLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__StoreLoc__E7FEA477CEAF8CAA");

                entity.ToTable("StoreLocation", "caproj0");

                entity.HasIndex(e => e.Phone)
                    .HasName("unique_Phone")
                    .IsUnique();

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.ManagerNavigation)
                    .WithMany(p => p.StoreLocation)
                    .HasForeignKey(d => d.Manager)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreLoca__Manag__71D1E811");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
