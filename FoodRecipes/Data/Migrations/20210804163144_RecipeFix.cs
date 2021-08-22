namespace FoodRecipes.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RecipeFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeToPrepare",
                table: "Recipes",
                newName: "CookingTime");

            migrationBuilder.RenameColumn(
                name: "PreparationGuiadance",
                table: "Recipes",
                newName: "Directions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Directions",
                table: "Recipes",
                newName: "PreparationGuiadance");

            migrationBuilder.RenameColumn(
                name: "CookingTime",
                table: "Recipes",
                newName: "TimeToPrepare");
        }
    }
}
