using Microsoft.EntityFrameworkCore.Migrations;

namespace MaxPizzaProject.Migrations
{
    public partial class CategorySize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategorySize_Categories_CategoryId",
                table: "CategorySize");

            migrationBuilder.DropForeignKey(
                name: "FK_CategorySize_Sizes_SizeId",
                table: "CategorySize");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategorySize",
                table: "CategorySize");

            migrationBuilder.RenameTable(
                name: "CategorySize",
                newName: "CategoriesSizes");

            migrationBuilder.RenameIndex(
                name: "IX_CategorySize_SizeId",
                table: "CategoriesSizes",
                newName: "IX_CategoriesSizes_SizeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriesSizes",
                table: "CategoriesSizes",
                columns: new[] { "CategoryId", "SizeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriesSizes_Categories_CategoryId",
                table: "CategoriesSizes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriesSizes_Sizes_SizeId",
                table: "CategoriesSizes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriesSizes_Categories_CategoryId",
                table: "CategoriesSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriesSizes_Sizes_SizeId",
                table: "CategoriesSizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriesSizes",
                table: "CategoriesSizes");

            migrationBuilder.RenameTable(
                name: "CategoriesSizes",
                newName: "CategorySize");

            migrationBuilder.RenameIndex(
                name: "IX_CategoriesSizes_SizeId",
                table: "CategorySize",
                newName: "IX_CategorySize_SizeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategorySize",
                table: "CategorySize",
                columns: new[] { "CategoryId", "SizeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySize_Categories_CategoryId",
                table: "CategorySize",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySize_Sizes_SizeId",
                table: "CategorySize",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
