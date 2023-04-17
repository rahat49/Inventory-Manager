using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory_Manager.Migrations
{
    public partial class category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
            name: "CategoryId",
            table: "Grossary",
            type: "int",
            nullable: false,
            defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Grossary_CategoryId",
                table: "Grossary",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grossary_Categories_CategoryId",
                table: "Grossary",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
              name: "FK_Grossary_Categories_CategoryId",
              table: "Grossary");

            migrationBuilder.DropIndex(
                name: "IX_Grossary_CategoryId",
                table: "Grossary");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Grossary");
        }
    }
}
