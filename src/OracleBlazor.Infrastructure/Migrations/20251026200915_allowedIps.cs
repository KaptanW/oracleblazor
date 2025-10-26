using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OracleBlazor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class allowedIps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ALLOWED_IPS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Ip = table.Column<string>(type: "NVARCHAR2(32)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    IsDeleted = table.Column<short>(type: "NUMBER(1)", nullable: false),
                    IsActive = table.Column<short>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALLOWED_IPS", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ALLOWED_IPS");
        }
    }
}
