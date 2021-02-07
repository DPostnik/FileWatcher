using Microsoft.EntityFrameworkCore.Migrations;

namespace project.Migrations
{
    public partial class _1_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "Notes",
                newName: "Price");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Notes",
                newName: "price");
        }
    }
}
