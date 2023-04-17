using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory_Manager.Migrations
{
    public partial class prod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<int>(
                name: "ProductGroupId",
                table: "Grossary",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductProfileId",
                table: "Grossary",
                type: "int",
                nullable: false,
                defaultValue: 0);

           

            migrationBuilder.CreateIndex(
                name: "IX_Grossary_ProductGroupId",
                table: "Grossary",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Grossary_ProductProfileId",
                table: "Grossary",
                column: "ProductProfileId");

           
            migrationBuilder.AddForeignKey(
                name: "FK_Grossary_ProductGroups_ProductGroupId",
                table: "Grossary",
                column: "ProductGroupId",
                principalTable: "ProductGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grossary_ProductProfiles_ProductProfileId",
                table: "Grossary",
                column: "ProductProfileId",
                principalTable: "ProductProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropForeignKey(
               name: "FK_Grossary_ProductGroup_ProductGroupId",
               table: "Grossary");

            migrationBuilder.DropIndex(
                name: "IX_Grossary_ProductGroupId",
                table: "Grossary");

            migrationBuilder.DropColumn(
                name: "ProductGroupId",
                table: "Grossary");

            migrationBuilder.DropForeignKey(
               name: "FK_Grossary_ProductProfile_ProductProfileId",
               table: "Grossary");

            migrationBuilder.DropIndex(
                name: "IX_Grossary_ProductProfileId",
                table: "Grossary");

            migrationBuilder.DropColumn(
                name: "ProductProfileId",
                table: "Grossary");
        }
    }
}
