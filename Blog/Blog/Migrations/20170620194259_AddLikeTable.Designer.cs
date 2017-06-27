using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BlogApp.Models;

namespace Blog.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20170620194259_AddLikeTable")]
    partial class AddLikeTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlogApp.Models.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body")
                        .IsRequired();

                    b.Property<int?>("CategoryId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("BlogId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("BlogApp.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName")
                        .IsRequired();

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BlogApp.Models.Like", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("BlogId");

                    b.Property<bool>("Liked");

                    b.Property<DateTime>("LikedAt");

                    b.HasKey("UserId", "BlogId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("BlogApp.Models.Blog", b =>
                {
                    b.HasOne("BlogApp.Models.Category", "Category")
                        .WithMany("Blogs")
                        .HasForeignKey("CategoryId");
                });
        }
    }
}
