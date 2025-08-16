using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBlogPostName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Author_AuthorId",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTags_Blog_BlogPostId",
                table: "BlogTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog",
                table: "Blog");

            migrationBuilder.RenameTable(
                name: "Blog",
                newName: "BlogPost");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_AuthorId",
                table: "BlogPost",
                newName: "IX_BlogPost_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPost",
                table: "BlogPost",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPost_Author_AuthorId",
                table: "BlogPost",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTags_BlogPost_BlogPostId",
                table: "BlogTags",
                column: "BlogPostId",
                principalTable: "BlogPost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPost_Author_AuthorId",
                table: "BlogPost");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTags_BlogPost_BlogPostId",
                table: "BlogTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPost",
                table: "BlogPost");

            migrationBuilder.RenameTable(
                name: "BlogPost",
                newName: "Blog");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPost_AuthorId",
                table: "Blog",
                newName: "IX_Blog_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog",
                table: "Blog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Author_AuthorId",
                table: "Blog",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTags_Blog_BlogPostId",
                table: "BlogTags",
                column: "BlogPostId",
                principalTable: "Blog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
