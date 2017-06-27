using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Migrations
{
    public partial class AddLike_Blog_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Likes_BlogId",
                table: "Likes",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Blogs_BlogId",
                table: "Likes",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Blogs_BlogId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_BlogId",
                table: "Likes");
        }
    }
}
