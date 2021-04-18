using Microsoft.EntityFrameworkCore.Migrations;

namespace SweatSpace.Api.Persistence.Migrations
{
    public partial class TimesCompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimesCompletedThisWeek",
                table: "Workouts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimesCompletedThisWeek",
                table: "Workouts");
        }
    }
}
