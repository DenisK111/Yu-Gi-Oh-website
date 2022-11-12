using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yu_Gi_Oh_website.Data.Migrations
{
    public partial class ChengedPropertyNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Posts");

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Threads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Posts");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Threads",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Posts",
                type: "bit",
                nullable: true,
                defaultValue: true);
        }
    }
}
