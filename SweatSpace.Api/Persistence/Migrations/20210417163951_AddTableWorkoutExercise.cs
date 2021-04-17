using Microsoft.EntityFrameworkCore.Migrations;

namespace SweatSpace.Api.Persistence.Migrations
{
    public partial class AddTableWorkoutExercise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercise_Exercises_ExerciseId",
                table: "WorkoutExercise");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercise_Workouts_WorkoutId",
                table: "WorkoutExercise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutExercise",
                table: "WorkoutExercise");

            migrationBuilder.RenameTable(
                name: "WorkoutExercise",
                newName: "WorkoutExercises");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutExercise_WorkoutId",
                table: "WorkoutExercises",
                newName: "IX_WorkoutExercises_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutExercise_ExerciseId",
                table: "WorkoutExercises",
                newName: "IX_WorkoutExercises_ExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutExercises",
                table: "WorkoutExercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_Exercises_ExerciseId",
                table: "WorkoutExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_Workouts_WorkoutId",
                table: "WorkoutExercises",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_Exercises_ExerciseId",
                table: "WorkoutExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_Workouts_WorkoutId",
                table: "WorkoutExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutExercises",
                table: "WorkoutExercises");

            migrationBuilder.RenameTable(
                name: "WorkoutExercises",
                newName: "WorkoutExercise");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutExercises_WorkoutId",
                table: "WorkoutExercise",
                newName: "IX_WorkoutExercise_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutExercises_ExerciseId",
                table: "WorkoutExercise",
                newName: "IX_WorkoutExercise_ExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutExercise",
                table: "WorkoutExercise",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercise_Exercises_ExerciseId",
                table: "WorkoutExercise",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercise_Workouts_WorkoutId",
                table: "WorkoutExercise",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
