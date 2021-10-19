using Microsoft.EntityFrameworkCore.Migrations;

namespace MaxPizzaProject.Migrations
{
    public partial class CategorySizePrice3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategorySize_Categories_CategoryId1",
                table: "CategorySize");

            migrationBuilder.DropForeignKey(
                name: "FK_CategorySize_Sizes_SizeId1",
                table: "CategorySize");

            migrationBuilder.DropIndex(
                name: "IX_CategorySize_CategoryId1",
                table: "CategorySize");

            migrationBuilder.DropIndex(
                name: "IX_CategorySize_SizeId1",
                table: "CategorySize");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "CategorySize");

            migrationBuilder.DropColumn(
                name: "SizeId1",
                table: "CategorySize");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryId1",
                table: "CategorySize",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SizeId1",
                table: "CategorySize",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategorySize_CategoryId1",
                table: "CategorySize",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySize_SizeId1",
                table: "CategorySize",
                column: "SizeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySize_Categories_CategoryId1",
                table: "CategorySize",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySize_Sizes_SizeId1",
                table: "CategorySize",
                column: "SizeId1",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
