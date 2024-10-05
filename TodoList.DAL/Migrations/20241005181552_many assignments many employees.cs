using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todolist.DAL.Migrations
{
    /// <inheritdoc />
    public partial class manyassignmentsmanyemployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Tasks_TaskId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TaskId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "AssignmentEmployee",
                columns: table => new
                {
                    EmployeesId = table.Column<Guid>(type: "uuid", nullable: false),
                    TasksId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentEmployee", x => new { x.EmployeesId, x.TasksId });
                    table.ForeignKey(
                        name: "FK_AssignmentEmployee_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentEmployee_Tasks_TasksId",
                        column: x => x.TasksId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentEmployee_TasksId",
                table: "AssignmentEmployee",
                column: "TasksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentEmployee");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskId",
                table: "Employees",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TaskId",
                table: "Employees",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Tasks_TaskId",
                table: "Employees",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
