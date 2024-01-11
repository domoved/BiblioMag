using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioMag.Migrations
{
    public partial class DeleteProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Properties",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Properties",
                table: "Books",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }
    }
}
