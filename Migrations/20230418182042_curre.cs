using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory_Manager.Migrations
{
    public partial class curre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExchangeCurrencyId",
                table: "Currencies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_ExchangeCurrencyId",
                table: "Currencies",
                column: "ExchangeCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Currencies_ExchangeCurrencyId",
                table: "Currencies",
                column: "ExchangeCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Currencies_ExchangeCurrencyId",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_ExchangeCurrencyId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "ExchangeCurrencyId",
                table: "Currencies");
        }
    }
}
