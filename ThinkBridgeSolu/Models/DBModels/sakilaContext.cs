using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ThinkBridgeSolu.Models.DBModels
{
    public partial class sakilaContext : DbContext
    {
        public sakilaContext()
        {
        }

        public sakilaContext(DbContextOptions<sakilaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=localhost;uid=root;pwd=Change123;database=sakila;port=3308");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.CategoryId, "fk_category");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.PriceCurrency)
                    .HasMaxLength(10)
                    .HasColumnName("price_currency");

                entity.Property(e => e.ProductDescription)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("product_description");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("product_name");

                entity.Property(e => e.ProductPrice)
                    .HasColumnType("decimal(6,2)")
                    .HasColumnName("product_price");

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_category");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PRIMARY");

                entity.ToTable("product_category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("category_name");

                entity.Property(e => e.ParentCategoryId).HasColumnName("parent_category_id");

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
