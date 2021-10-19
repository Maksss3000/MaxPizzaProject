using Microsoft.EntityFrameworkCore.Migrations;

namespace MaxPizzaProject.Migrations
{
    public partial class SizesPrices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Sizes_SizeId",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_SizeId",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Prices");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceSize");

            migrationBuilder.AddColumn<long>(
                name: "SizeId",
                table: "Prices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Prices_SizeId",
                table: "Prices",
                column: "SizeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Sizes_SizeId",
                table: "Prices",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
