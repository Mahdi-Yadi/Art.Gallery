using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Art.Gallery.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateUserAddrress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Addrress",
                schema: "ArtGallery",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodePost",
                schema: "ArtGallery",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "ArtGallery",
                table: "Users",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Addrress",
                schema: "ArtGallery",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CodePost",
                schema: "ArtGallery",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "ArtGallery",
                table: "Users");
        }
    }
}
