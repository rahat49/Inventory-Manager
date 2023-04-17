using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory_Manager.Migrations
{
    public partial class @for : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
             name: "BrandId",
             table: "Grossary",
             type: "int",
             nullable: false,
             defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Grossary_BrandId",
                table: "Grossary",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grossary_Brands_BrandId",
                table: "Grossary",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grossary_Brands_BrandId",
                table: "Grossary");

            migrationBuilder.DropIndex(
                name: "IX_Grossary_BrandId",
                table: "Grossary");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Grossary");

        }
    }
}
