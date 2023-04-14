using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_crud_mvc.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IngredientId",
                table: "Pizzas",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pizzas_IngredientId",
                table: "Pizzas",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_Ingredients_IngredientId",
                table: "Pizzas",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_Ingredients_IngredientId",
                table: "Pizzas");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Pizzas_IngredientId",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "Pizzas");
        }
    }
}
