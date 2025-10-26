using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OracleBlazor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ASSETS_TAG",
                table: "ASSETS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ASSETS_TAG",
                table: "ASSETS",
                column: "Tag",
                unique: true);
        }
    }
}
