using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todolist.DAL.Migrations
{
    /// <inheritdoc />
    public partial class adddone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Done",
                table: "Tasks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Done",
                table: "Tasks");
        }
    }
}
