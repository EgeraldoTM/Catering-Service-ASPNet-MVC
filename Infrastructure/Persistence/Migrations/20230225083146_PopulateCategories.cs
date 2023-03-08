using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CateringService.Migrations
{
    public partial class PopulateCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("INSERT INTO Categories (Id, Name) VALUES (1, 'Salads')");
			migrationBuilder.Sql("INSERT INTO Categories (Id, Name) VALUES (2, 'Entrees')");
			migrationBuilder.Sql("INSERT INTO Categories (Id, Name) VALUES (3, 'Deserts')");
			migrationBuilder.Sql("INSERT INTO Categories (Id, Name) VALUES (4, 'Beverages')");
			migrationBuilder.Sql("INSERT INTO Categories (Id, Name) VALUES (5, 'Fruits')");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
