using Core.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CateringService.Migrations
{
    public partial class SeedFoodItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("INSERT INTO FoodItems (Name, Description, CategoryId, Price, IsDeleted) VALUES ('Cesar Salad', 'Green salad of romaine lettuce and croutons dressed with lemon juice, olive oil, egg, Worcestershire sauce, anchovies, garlic, Parmesan cheese, and black pepper.', 1, 4.99, 0)");
			migrationBuilder.Sql("INSERT INTO FoodItems (Name, Description, CategoryId, Price, IsDeleted) VALUES ('Roasted Chicken', 'Chicken, salt, pepper, butter, celery. Cooked in oven.', 2, 17.49, 0)");
			migrationBuilder.Sql("INSERT INTO FoodItems (Name, Description, CategoryId, Price, IsDeleted) VALUES ('Brownie', 'Butter, white sugar, eggs, vanilla extract, cocoa powder, all-purpose flour, salt, and baking powder.', 3, 5.99, 0)");
			migrationBuilder.Sql("INSERT INTO FoodItems (Name, Description, CategoryId, Price, IsDeleted) VALUES ('Espresso', 'Normal Espresso.', 4, 2.49, 0)");
			migrationBuilder.Sql("INSERT INTO FoodItems (Name, Description, CategoryId, Price, IsDeleted) VALUES ('Fruit Mix', 'Pineapple, oranges, strawberries, bananas, seedles grapes.', 5, 9.99, 0)");
			migrationBuilder.Sql("INSERT INTO FoodItems (Name, Description, CategoryId, Price, IsDeleted) VALUES ('Apples', '3 Fresh green, red or yellow apples.', 5, 3.99, 0)");
		}
	protected override void Down(MigrationBuilder migrationBuilder)
	{
	}
    }
}
