using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Art.Gallery.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatetoken1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                schema: "ArtGallery",
                table: "RefreshTokens");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "ArtGallery",
                table: "RefreshTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "ArtGallery",
                table: "RefreshTokens");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                schema: "ArtGallery",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
