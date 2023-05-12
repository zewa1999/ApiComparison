using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiComparison.EfCore.Persistence.ApiComparison.EfCore.Persistence
{
    /// <inheritdoc />
    public partial class UserHasDishes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishIngredient_Dish_DishIngredientsId",
                table: "DishIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_DishIngredient_Ingredient_DishIngredientsId1",
                table: "DishIngredient");

            migrationBuilder.RenameColumn(
                name: "DishIngredientsId1",
                table: "DishIngredient",
                newName: "IngredientsId");

            migrationBuilder.RenameColumn(
                name: "DishIngredientsId",
                table: "DishIngredient",
                newName: "DishesId");

            migrationBuilder.RenameIndex(
                name: "IX_DishIngredient_DishIngredientsId1",
                table: "DishIngredient",
                newName: "IX_DishIngredient_IngredientsId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Address",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_User_UserId",
                table: "Address",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DishIngredient_Dish_DishesId",
                table: "DishIngredient",
                column: "DishesId",
                principalTable: "Dish",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishIngredient_Ingredient_IngredientsId",
                table: "DishIngredient",
                column: "IngredientsId",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_User_UserId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_DishIngredient_Dish_DishesId",
                table: "DishIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_DishIngredient_Ingredient_IngredientsId",
                table: "DishIngredient");

            migrationBuilder.DropIndex(
                name: "IX_Address_UserId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "IngredientsId",
                table: "DishIngredient",
                newName: "DishIngredientsId1");

            migrationBuilder.RenameColumn(
                name: "DishesId",
                table: "DishIngredient",
                newName: "DishIngredientsId");

            migrationBuilder.RenameIndex(
                name: "IX_DishIngredient_IngredientsId",
                table: "DishIngredient",
                newName: "IX_DishIngredient_DishIngredientsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DishIngredient_Dish_DishIngredientsId",
                table: "DishIngredient",
                column: "DishIngredientsId",
                principalTable: "Dish",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishIngredient_Ingredient_DishIngredientsId1",
                table: "DishIngredient",
                column: "DishIngredientsId1",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
