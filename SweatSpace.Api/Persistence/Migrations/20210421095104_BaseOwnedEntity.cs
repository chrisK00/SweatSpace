using Microsoft.EntityFrameworkCore.Migrations;

namespace SweatSpace.Api.Persistence.Migrations
{
    public partial class BaseOwnedEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "WorkoutExercises",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_AppUserId",
                table: "WorkoutExercises",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_AspNetUsers_AppUserId",
                table: "WorkoutExercises",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_AspNetUsers_AppUserId",
                table: "WorkoutExercises");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutExercises_AppUserId",
                table: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "WorkoutExercises");
        }
    }
}
