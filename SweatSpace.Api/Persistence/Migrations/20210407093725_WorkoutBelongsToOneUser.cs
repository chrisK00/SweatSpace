using Microsoft.EntityFrameworkCore.Migrations;

namespace SweatSpace.Api.Persistence.Migrations
{
    public partial class WorkoutBelongsToOneUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserWorkout_Users_UsersId",
                table: "AppUserWorkout");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserWorkout_Workouts_WorkoutsId",
                table: "AppUserWorkout");

            migrationBuilder.DropTable(
                name: "AppUserWorkout1");

            migrationBuilder.RenameColumn(
                name: "WorkoutsId",
                table: "AppUserWorkout",
                newName: "UsersThatLikedId");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "AppUserWorkout",
                newName: "LikedWorkoutsId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserWorkout_WorkoutsId",
                table: "AppUserWorkout",
                newName: "IX_AppUserWorkout_UsersThatLikedId");

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
                name: "FK_AppUserWorkout_Users_UsersThatLikedId",
                table: "AppUserWorkout",
                column: "UsersThatLikedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserWorkout_Workouts_LikedWorkoutsId",
                table: "AppUserWorkout",
                column: "LikedWorkoutsId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Users_UserId",
                table: "Workouts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserWorkout_Users_UsersThatLikedId",
                table: "AppUserWorkout");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserWorkout_Workouts_LikedWorkoutsId",
                table: "AppUserWorkout");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Users_UserId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Workouts");

            migrationBuilder.RenameColumn(
                name: "UsersThatLikedId",
                table: "AppUserWorkout",
                newName: "WorkoutsId");

            migrationBuilder.RenameColumn(
                name: "LikedWorkoutsId",
                table: "AppUserWorkout",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserWorkout_UsersThatLikedId",
                table: "AppUserWorkout",
                newName: "IX_AppUserWorkout_WorkoutsId");

            migrationBuilder.CreateTable(
                name: "AppUserWorkout1",
                columns: table => new
                {
                    LikedWorkoutsId = table.Column<int>(type: "integer", nullable: false),
                    UsersThatLikedId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserWorkout1", x => new { x.LikedWorkoutsId, x.UsersThatLikedId });
                    table.ForeignKey(
                        name: "FK_AppUserWorkout1_Users_UsersThatLikedId",
                        column: x => x.UsersThatLikedId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserWorkout1_Workouts_LikedWorkoutsId",
                        column: x => x.LikedWorkoutsId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserWorkout1_UsersThatLikedId",
                table: "AppUserWorkout1",
                column: "UsersThatLikedId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserWorkout_Users_UsersId",
                table: "AppUserWorkout",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserWorkout_Workouts_WorkoutsId",
                table: "AppUserWorkout",
                column: "WorkoutsId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
