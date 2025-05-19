using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Art.Gallery.Data.Migrations
{
    /// <inheritdoc />
    public partial class aas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase(
                collation: "Persian_100_CI_AS_SC_UTF8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase(
                oldCollation: "Persian_100_CI_AS_SC_UTF8");
        }
    }
}
