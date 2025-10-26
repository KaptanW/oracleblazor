using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OracleBlazor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ASSETS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Tag = table.Column<string>(type: "NVARCHAR2(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    Category = table.Column<string>(type: "NVARCHAR2(32)", maxLength: 32, nullable: true),
                    Location = table.Column<string>(type: "NVARCHAR2(32)", maxLength: 32, nullable: true),
                    Status = table.Column<string>(type: "NVARCHAR2(32)", maxLength: 32, nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    Cost = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    IsDeleted = table.Column<short>(type: "NUMBER(1)", nullable: false),
                    IsActive = table.Column<short>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASSETS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    UserName = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    RealName = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    IsDeleted = table.Column<short>(type: "NUMBER(1)", nullable: false),
                    IsActive = table.Column<short>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ASSETS_CAT_LOC",
                table: "ASSETS",
                columns: new[] { "Category", "Location" });

            migrationBuilder.CreateIndex(
                name: "IX_ASSETS_TAG",
                table: "ASSETS",
                column: "Tag",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASSETS");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
