using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yu_Gi_Oh_website.Web.Data.Migrations
{
    public partial class fixedAttributeLength_and_RemovedArchetypeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archetype",
                table: "Cards");

            migrationBuilder.AlterColumn<string>(
                name: "Attribute",
                table: "Cards",
                type: "VARCHAR(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Attribute",
                table: "Cards",
                type: "VARCHAR",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Archetype",
                table: "Cards",
                type: "VARCHAR",
                nullable: true);
        }
    }
}
