using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yu_Gi_Oh_website.Data.Migrations
{
    public partial class fixedcardentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CardAttributes_CardAttributeId1",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Types_TypeId1",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CardAttributeId1",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_TypeId1",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CardAttributeId1",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CardTypeId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "TypeId1",
                table: "Cards");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Cards",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CardAttributeId",
                table: "Cards",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardAttributeId",
                table: "Cards",
                column: "CardAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_TypeId",
                table: "Cards",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CardAttributes_CardAttributeId",
                table: "Cards",
                column: "CardAttributeId",
                principalTable: "CardAttributes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Types_TypeId",
                table: "Cards",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CardAttributes_CardAttributeId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Types_TypeId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CardAttributeId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_TypeId",
                table: "Cards");

            migrationBuilder.AlterColumn<string>(
                name: "TypeId",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CardAttributeId",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardAttributeId1",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardTypeId",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TypeId1",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardAttributeId1",
                table: "Cards",
                column: "CardAttributeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_TypeId1",
                table: "Cards",
                column: "TypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CardAttributes_CardAttributeId1",
                table: "Cards",
                column: "CardAttributeId1",
                principalTable: "CardAttributes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Types_TypeId1",
                table: "Cards",
                column: "TypeId1",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
