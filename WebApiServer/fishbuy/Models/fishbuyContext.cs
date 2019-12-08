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
        public virtual DbSet<MediaLink> MediaLink { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("COMMENT", "fishbuy");

                entity.HasIndex(e => e.PostId)
                    .HasName("POST_ID");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID");

                entity.Property(e => e.CommentId)
                    .HasColumnName("COMMENT_ID")
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("CONTENT")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PostId)
                    .HasColumnName("POST_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpTime)
                    .IsRequired()
                    .HasColumnName("UP_TIME")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("USER_ID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("COMMENT_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("COMMENT_ibfk_2");
            });

            modelBuilder.Entity<MediaLink>(entity =>
            {
                entity.HasKey(e => e.MediaId);

                entity.ToTable("MEDIA_LINK", "fishbuy");

                entity.HasIndex(e => e.PostId)
                    .HasName("POST_ID");

                entity.Property(e => e.MediaId)
                    .HasColumnName("MEDIA_ID")
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.PostId)
                    .HasColumnName("POST_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ResType)
                    .IsRequired()
                    .HasColumnName("RES_TYPE")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ResUri)
                    .IsRequired()
                    .HasColumnName("RES_URI")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.MediaLink)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MEDIA_LINK_ibfk_1");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("POST", "fishbuy");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID");

                entity.Property(e => e.PostId)
                    .HasColumnName("POST_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("ADDRESS")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("CONTENT")
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
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("POST_ibfk_1");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Token);

                entity.ToTable("REFRESH_TOKEN", "fishbuy");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID");

                entity.Property(e => e.Token)
                    .HasColumnName("TOKEN")
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Expires).HasColumnName("EXPIRES");

                entity.Property(e => e.UserId)
                    .HasColumnName("USER_ID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshToken)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("REFRESH_TOKEN_ibfk_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER", "fishbuy");

                entity.Property(e => e.UserId)
                    .HasColumnName("USER_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnName("ADDRESS")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate)
                    .HasColumnName("BIRTH_DATE")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("IMAGE_URL")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasColumnName("NICKNAME")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("PASSWORD_HASH")
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

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("USERNAME")
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });
        }
    }
}
