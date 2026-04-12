using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace uvlo_api.Migrations
{
    /// <inheritdoc />
    public partial class UnifiedPostModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppPostComments");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AppPosts");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "AppPosts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AppPosts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppPosts_ParentId",
                table: "AppPosts",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPosts_UserId",
                table: "AppPosts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPosts_AppPosts_ParentId",
                table: "AppPosts",
                column: "ParentId",
                principalTable: "AppPosts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPosts_AspNetUsers_UserId",
                table: "AppPosts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPosts_AppPosts_ParentId",
                table: "AppPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPosts_AspNetUsers_UserId",
                table: "AppPosts");

            migrationBuilder.DropIndex(
                name: "IX_AppPosts_ParentId",
                table: "AppPosts");

            migrationBuilder.DropIndex(
                name: "IX_AppPosts_UserId",
                table: "AppPosts");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "AppPosts");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppPosts");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AppPosts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AppPostComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    PostId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPostComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPostComments_AppPosts_PostId",
                        column: x => x.PostId,
                        principalTable: "AppPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppPostComments_PostId",
                table: "AppPostComments",
                column: "PostId");
        }
    }
}
