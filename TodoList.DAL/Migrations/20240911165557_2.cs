using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todolist.DAL.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Tasks_TaskId",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Tasks_TaskId",
                table: "Employees",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Tasks_TaskId",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Tasks_TaskId",
                table: "Employees",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
