namespace FoodRecipes.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class DisengageCookFromRecipeForNow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Cook_CookId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "Cook");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_CookId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CookId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CookId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cook_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CookId",
                table: "Recipes",
                column: "CookId");

            migrationBuilder.CreateIndex(
                name: "IX_Cook_UserId",
                table: "Cook",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Cook_CookId",
                table: "Recipes",
                column: "CookId",
                principalTable: "Cook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
