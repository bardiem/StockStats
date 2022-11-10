using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockStats.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Symbols",
                columns: table => new
                {
                    SymbolID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SymbolName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartSaleDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symbols", x => x.SymbolID);
                });

            migrationBuilder.CreateTable(
                name: "SymbolPerformance",
                columns: table => new
                {
                    SymbolPerformanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SymbolID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AveragePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymbolPerformance", x => x.SymbolPerformanceID);
                    table.ForeignKey(
                        name: "FK_SymbolPerformance_Symbols_SymbolID",
                        column: x => x.SymbolID,
                        principalTable: "Symbols",
                        principalColumn: "SymbolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SymbolPerformance_SymbolID",
                table: "SymbolPerformance",
                column: "SymbolID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SymbolPerformance");

            migrationBuilder.DropTable(
                name: "Symbols");
        }
    }
}
