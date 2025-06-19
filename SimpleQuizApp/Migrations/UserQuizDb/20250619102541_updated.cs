using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleQuizApp.Migrations.UserQuizDb
{
    /// <inheritdoc />
    public partial class updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "chosenOption",
                table: "Questions",
                newName: "ChosenOption");

            migrationBuilder.AlterColumn<int>(
                name: "ChosenOption",
                table: "Questions",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChosenOption",
                table: "Questions",
                newName: "chosenOption");

            migrationBuilder.AlterColumn<string>(
                name: "chosenOption",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
