using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yu_Gi_Oh_website.Data.Migrations
{
    public partial class removedThreadVotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThreadVotes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThreadVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThreadId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsUpvote = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadVotes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThreadVotes_Threads_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThreadVotes_IsDeleted",
                table: "ThreadVotes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadVotes_ThreadId",
                table: "ThreadVotes",
                column: "ThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadVotes_UserId",
                table: "ThreadVotes",
                column: "UserId");
        }
    }
}
