using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace fishbuy.Models
{
    public partial class FishbuyContext : DbContext
    {
        public FishbuyContext()
        {
        }

        public FishbuyContext(DbContextOptions<FishbuyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Goods> Goods { get; set; }
        public virtual DbSet<User> User { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseMySQL("Server=localhost;User Id=miho;Password=Qq!11111;Database=fishbuy");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("COMMENT", "fishbuy");

                entity.HasIndex(e => e.GoodsId)
                    .HasName("GOODS_ID");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID");

                entity.Property(e => e.CommentId)
                    .HasColumnName("COMMENT_ID")
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Comment1)
                    .IsRequired()
                    .HasColumnName("COMMENT")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.GoodsId)
                    .HasColumnName("GOODS_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpTime)
                    .IsRequired()
                    .HasColumnName("UP_TIME")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("USER_ID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Goods)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.GoodsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("COMMENT_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("COMMENT_ibfk_2");
            });

            modelBuilder.Entity<Goods>(entity =>
            {
                entity.ToTable("GOODS", "fishbuy");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID");

                entity.Property(e => e.GoodsId)
                    .HasColumnName("GOODS_ID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("ADDRESS")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.EditTime)
                    .IsRequired()
                    .HasColumnName("EDIT_TIME")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("PRICE")
                    .HasColumnType("decimal(14,2)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("STATUS")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Tags)
                    .HasColumnName("TAGS")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("TITLE")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.UpTime)
                    .IsRequired()
                    .HasColumnName("UP_TIME")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("USER_ID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Goods)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GOODS_ibfk_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER", "fishbuy");

                entity.Property(e => e.UserId)
                    .HasColumnName("USER_ID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("ADDRESS")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Birthdate)
                    .HasColumnName("BIRTHDATE")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasColumnName("NICKNAME")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("PHONE")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasColumnName("SEX")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("USER_NAME")
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });
        }
    }
}
