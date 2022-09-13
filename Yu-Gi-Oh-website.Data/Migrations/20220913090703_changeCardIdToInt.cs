using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yu_Gi_Oh_website.Data.Migrations
{
    public partial class changeCardIdToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardImages_Cards_CardId1",
                table: "CardImages");

            migrationBuilder.DropIndex(
                name: "IX_CardImages_CardId1",
                table: "CardImages");

            migrationBuilder.DropColumn(
                name: "CardId1",
                table: "CardImages");

            migrationBuilder.AlterColumn<int>(
                name: "CardId",
                table: "CardImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_CardImages_CardId",
                table: "CardImages",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardImages_Cards_CardId",
                table: "CardImages",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardImages_Cards_CardId",
                table: "CardImages");

            migrationBuilder.DropIndex(
                name: "IX_CardImages_CardId",
                table: "CardImages");

            migrationBuilder.AlterColumn<string>(
                name: "CardId",
                table: "CardImages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CardId1",
                table: "CardImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CardImages_CardId1",
                table: "CardImages",
                column: "CardId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CardImages_Cards_CardId1",
                table: "CardImages",
                column: "CardId1",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
