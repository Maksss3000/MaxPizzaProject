using Microsoft.EntityFrameworkCore.Migrations;

namespace MaxPizzaProject.Migrations
{
    public partial class InStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Pizzas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Pizzas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Pizzas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Pizzas");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
