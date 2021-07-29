using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorBlog.WebApi.Data.Migrations
{
    public partial class ManyToManyEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GithubLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntroPostContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchVersion = table.Column<float>(type: "real", nullable: false),
                    FrontPostImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostTags",
                columns: table => new
                {
                    TagText = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => x.TagText);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostPostTag",
                columns: table => new
                {
                    PostsId = table.Column<int>(type: "int", nullable: false),
                    TagsTagText = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostPostTag", x => new { x.PostsId, x.TagsTagText });
                    table.ForeignKey(
                        name: "FK_BlogPostPostTag_BlogPosts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostPostTag_PostTags_TagsTagText",
                        column: x => x.TagsTagText,
                        principalTable: "PostTags",
                        principalColumn: "TagText",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostPostTag_TagsTagText",
                table: "BlogPostPostTag",
                column: "TagsTagText");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostPostTag");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "PostTags");
        }
    }
}
