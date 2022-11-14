using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockStats.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Symbols");

            migrationBuilder.DropColumn(
                name: "StartSaleDate",
                table: "Symbols");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "SymbolPerformance",
                newName: "PerformanceDateTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Symbols",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "SymbolPerformance",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateFrequency",
                table: "SymbolPerformance",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Symbols");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "SymbolPerformance");

            migrationBuilder.DropColumn(
                name: "UpdateFrequency",
                table: "SymbolPerformance");

            migrationBuilder.RenameColumn(
                name: "PerformanceDateTime",
                table: "SymbolPerformance",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Symbols",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartSaleDate",
                table: "Symbols",
                type: "datetime2",
                nullable: true);
        }
    }
}
