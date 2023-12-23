using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApp.Migrations
{
    public partial class Currency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "BankWallets");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "TimeLImit",
                table: "Loans",
                newName: "TimeLimit");

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                table: "Loans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                table: "BankWallets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CurrencyId",
                table: "Transactions",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_CurrencyId",
                table: "Loans",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_BankWallets_CurrencyId",
                table: "BankWallets",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CurrencyId",
                table: "Accounts",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Currencies_CurrencyId",
                table: "Accounts",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankWallets_Currencies_CurrencyId",
                table: "BankWallets",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Currencies_CurrencyId",
                table: "Loans",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Currencies_CurrencyId",
                table: "Transactions",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Currencies_CurrencyId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BankWallets_Currencies_CurrencyId",
                table: "BankWallets");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Currencies_CurrencyId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Currencies_CurrencyId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CurrencyId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Loans_CurrencyId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_BankWallets_CurrencyId",
                table: "BankWallets");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CurrencyId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "BankWallets");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "TimeLimit",
                table: "Loans",
                newName: "TimeLImit");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "BankWallets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
