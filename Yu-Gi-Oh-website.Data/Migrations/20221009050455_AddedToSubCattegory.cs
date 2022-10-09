using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yu_Gi_Oh_website.Data.Migrations
{
    public partial class AddedToSubCattegory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastPostAuthor",
                table: "SubCattegories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastThreadName",
                table: "SubCattegories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "PostContents",
                type: "nvarchar(max)",
                maxLength: 6000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPostAuthor",
                table: "SubCattegories");

            migrationBuilder.DropColumn(
                name: "LastThreadName",
                table: "SubCattegories");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "PostContents",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 6000);
        }
    }
}
