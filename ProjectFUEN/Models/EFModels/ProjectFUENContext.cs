// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjectFUEN.Models.DTOs;

namespace ProjectFUEN.Models.EFModels
{
    public partial class ProjectFUENContext : DbContext
    {
        public ProjectFUENContext()
        {
        }

        public ProjectFUENContext(DbContextOptions<ProjectFUENContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductPhoto> ProductPhotos { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Discount).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasMany(d => d.Products)
                    .WithMany(p => p.Events)
                    .UsingEntity<Dictionary<string, object>>(
                        "EventItem",
                        l => l.HasOne<Product>().WithMany().HasForeignKey("ProductId").HasConstraintName("FK__EventItem__Produ__46E78A0C"),
                        r => r.HasOne<Event>().WithMany().HasForeignKey("EventId").HasConstraintName("FK__EventItem__Event__45F365D3"),
                        j =>
                        {
                            j.HasKey("EventId", "ProductId").HasName("PK__EventIte__B204047CA3A22E41");

                            j.ToTable("EventItems");
                        });
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasIndex(e => e.EmailAccount, "UQ__Members__005407CD01E219D8")
                    .IsUnique();

                entity.Property(e => e.About).HasMaxLength(500);

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.BirthOfDate).HasColumnType("date");

                entity.Property(e => e.ConfirmCode)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.EmailAccount)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EncryptedPassword)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Mobile)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NickName).HasMaxLength(50);

                entity.Property(e => e.PhotoSticker)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.RealName).HasMaxLength(50);

                entity.HasMany(d => d.Coupons)
                    .WithMany(p => p.Members)
                    .UsingEntity<Dictionary<string, object>>(
                        "UsedCoupon",
                        l => l.HasOne<Coupon>().WithMany().HasForeignKey("CouponId").HasConstraintName("FK__UsedCoupo__Coupo__4CA06362"),
                        r => r.HasOne<Member>().WithMany().HasForeignKey("MemberId").HasConstraintName("FK__UsedCoupo__Membe__4BAC3F29"),
                        j =>
                        {
                            j.HasKey("MemberId", "CouponId").HasName("PK__UsedCoup__BF74E40319F00B13");

                            j.ToTable("UsedCoupons");
                        });

                entity.HasMany(d => d.Products)
                    .WithMany(p => p.Members)
                    .UsingEntity<Dictionary<string, object>>(
                        "Favorite",
                        l => l.HasOne<Product>().WithMany().HasForeignKey("ProductId").HasConstraintName("FK__Favorites__Produ__35BCFE0A"),
                        r => r.HasOne<Member>().WithMany().HasForeignKey("MemberId").HasConstraintName("FK__Favorites__Membe__34C8D9D1"),
                        j =>
                        {
                            j.HasKey("MemberId", "ProductId").HasName("PK__Favorite__C7B08774A7A81927");

                            j.ToTable("Favorites");
                        });
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__OrderDeta__Membe__3D5E1FD2");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK__OrderIte__08D097A37EFF7133");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderItem__Order__403A8C7D");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__OrderItem__Produ__412EB0B6");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ManufactorDate).HasColumnType("datetime");

                entity.Property(e => e.ProductSpec)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__BrandI__2F10007B");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__Catego__2E1BDC42");
            });

            modelBuilder.Entity<ProductPhoto>(entity =>
            {
                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductPhotos)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ProductPh__Produ__31EC6D26");
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.ProductId })
                    .HasName("PK__Shopping__C7B08774CDA42069");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__ShoppingC__Membe__38996AB5");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ShoppingC__Produ__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        //public DbSet<ProjectFUEN.Models.DTOs.OrderDetailsDTO> OrderDetailsDTO { get; set; }
    }
}