using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace uvlo_api.Migrations
{
    /// <inheritdoc />
    public partial class PostsRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "AppPosts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppPosts",
                table: "AppPosts",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppPosts",
                table: "AppPosts");

            migrationBuilder.RenameTable(
                name: "AppPosts",
                newName: "Posts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");
        }
    }
}
