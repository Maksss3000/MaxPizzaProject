using Microsoft.EntityFrameworkCore.Migrations;

namespace MaxPizzaProject.Migrations
{
    public partial class CategorySizePrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategorySize_Categories_CategoriesId",
                table: "CategorySize");

            migrationBuilder.DropForeignKey(
                name: "FK_CategorySize_Sizes_SizesId",
                table: "CategorySize");

            migrationBuilder.DropTable(
                name: "PriceSize");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.RenameColumn(
                name: "SizesId",
                table: "CategorySize",
                newName: "SizeId");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "CategorySize",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CategorySize_SizesId",
                table: "CategorySize",
                newName: "IX_CategorySize_SizeId");

            migrationBuilder.AddColumn<long>(
                name: "CategoryId1",
                table: "CategorySize",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CategorySize",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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
                name: "FK_CategorySize_Categories_CategoryId",
                table: "CategorySize",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySize_Categories_CategoryId1",
                table: "CategorySize",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySize_Sizes_SizeId",
                table: "CategorySize",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySize_Sizes_SizeId1",
                table: "CategorySize",
                column: "SizeId1",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategorySize_Categories_CategoryId",
                table: "CategorySize");

            migrationBuilder.DropForeignKey(
                name: "FK_CategorySize_Categories_CategoryId1",
                table: "CategorySize");

            migrationBuilder.DropForeignKey(
                name: "FK_CategorySize_Sizes_SizeId",
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
                name: "Price",
                table: "CategorySize");

            migrationBuilder.DropColumn(
                name: "SizeId1",
                table: "CategorySize");

            migrationBuilder.RenameColumn(
                name: "SizeId",
                table: "CategorySize",
                newName: "SizesId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "CategorySize",
                newName: "CategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_CategorySize_SizeId",
                table: "CategorySize",
                newName: "IX_CategorySize_SizesId");

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThePrice = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceSize",
                columns: table => new
                {
                    PricesId = table.Column<long>(type: "bigint", nullable: false),
                    SizesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceSize", x => new { x.PricesId, x.SizesId });
                    table.ForeignKey(
                        name: "FK_PriceSize_Prices_PricesId",
                        column: x => x.PricesId,
                        principalTable: "Prices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceSize_Sizes_SizesId",
                        column: x => x.SizesId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceSize_SizesId",
                table: "PriceSize",
                column: "SizesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySize_Categories_CategoriesId",
                table: "CategorySize",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySize_Sizes_SizesId",
                table: "CategorySize",
                column: "SizesId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
