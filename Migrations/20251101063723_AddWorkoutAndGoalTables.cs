using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutGoalApi.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkoutAndGoalTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Gid = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GoalType = table.Column<string>(type: "TEXT", nullable: false),
                    TargetValue = table.Column<double>(type: "REAL", nullable: false),
                    CurrentValue = table.Column<double>(type: "REAL", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId1 = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Gid);
                    table.ForeignKey(
                        name: "FK_Goals_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Wid = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WorkoutType = table.Column<string>(type: "TEXT", nullable: false),
                    Detail = table.Column<string>(type: "TEXT", nullable: false),
                    DurationMinute = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId1 = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Wid);
                    table.ForeignKey(
                        name: "FK_Workouts_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goals_UserId1",
                table: "Goals",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_UserId1",
                table: "Workouts",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_WorkoutType",
                table: "Workouts",
                column: "WorkoutType",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Workouts");
        }
    }
}
