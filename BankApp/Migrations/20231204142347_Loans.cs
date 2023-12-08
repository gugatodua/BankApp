using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApp.Migrations
{
    public partial class Loans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PaymentDay",
                table: "Loans",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

          

            migrationBuilder.AddColumn<int>(
                name: "TimeLImit",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "TimeLImit",
                table: "Loans");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDay",
                table: "Loans",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
