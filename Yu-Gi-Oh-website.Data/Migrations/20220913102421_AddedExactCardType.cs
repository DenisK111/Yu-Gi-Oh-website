using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yu_Gi_Oh_website.Data.Migrations
{
    public partial class AddedExactCardType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExactCardTypeId",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExactCardTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExactCardTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_ExactCardTypeId",
                table: "Cards",
                column: "ExactCardTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_ExactCardTypes_ExactCardTypeId",
                table: "Cards",
                column: "ExactCardTypeId",
                principalTable: "ExactCardTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_ExactCardTypes_ExactCardTypeId",
                table: "Cards");

            migrationBuilder.DropTable(
                name: "ExactCardTypes");

            migrationBuilder.DropIndex(
                name: "IX_Cards_ExactCardTypeId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "ExactCardTypeId",
                table: "Cards");
        }
    }
}
