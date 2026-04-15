using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace uvlo_api.Migrations
{
    /// <inheritdoc />
    public partial class ContentMaxLength600 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "AppPosts",
                type: "character varying(600)",
                maxLength: 600,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(550)",
                oldMaxLength: 550);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "AppPosts",
                type: "character varying(550)",
                maxLength: 550,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(600)",
                oldMaxLength: 600);
        }
    }
}
