using Microsoft.EntityFrameworkCore.Migrations;

namespace SweatSpace.Api.Persistence.Migrations
{
    public partial class ConfigureUserAndWorkouts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_AspNetUsers_UserId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Workouts");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Workouts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_AppUserId",
                table: "Workouts",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_AspNetUsers_AppUserId",
                table: "Workouts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_AspNetUsers_AppUserId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_AppUserId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Workouts");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Workouts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_AspNetUsers_UserId",
                table: "Workouts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
