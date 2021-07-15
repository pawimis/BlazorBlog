using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BlazorBlog.WebApi.Data.Entities
{
    public partial class BlazorBlogContext : DbContext
    {
        public BlazorBlogContext()
        {
        }

        public BlazorBlogContext(DbContextOptions<BlazorBlogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BlogPost> BlogPosts { get; set; }
        public virtual DbSet<PostTagRelation> PostTagRelations { get; set; }
        public virtual DbSet<TagsTable> TagsTables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.Property(e => e.BranchVersion).HasColumnType("decimal(5, 3)");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FrontPostImage).HasMaxLength(250);

                entity.Property(e => e.IntroPostContent).HasMaxLength(600);

                entity.Property(e => e.Title).HasMaxLength(200);
            });

            modelBuilder.Entity<PostTagRelation>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.Post)
                    .WithMany()
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostTagRelations_BlogPosts");

                entity.HasOne(d => d.Tag)
                    .WithMany()
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostTagRelations_TagsTable");
            });

            modelBuilder.Entity<TagsTable>(entity =>
            {
                entity.ToTable("TagsTable");

                entity.Property(e => e.TagText).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
