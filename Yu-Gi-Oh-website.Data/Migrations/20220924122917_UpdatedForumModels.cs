using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yu_Gi_Oh_website.Data.Migrations
{
    public partial class UpdatedForumModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_AuthorId1",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Threads_ForumThreadId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCattegories_Cattegories_CattegoryId",
                table: "SubCattegories");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_AspNetUsers_AuthorId1",
                table: "Threads");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_SubCattegories_SubCattegoryId",
                table: "Threads");

            migrationBuilder.DropIndex(
                name: "IX_Threads_AuthorId1",
                table: "Threads");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AuthorId1",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ForumThreadId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ForumThreadId",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "SubCattegoryId",
                table: "Threads",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "Threads",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Threads",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Threads",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "CattegoryId",
                table: "SubCattegories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Posts",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "ThreadId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "LinkValue",
                table: "Cards",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Cards",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostsCount",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_AuthorId",
                table: "Threads",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ThreadId",
                table: "Posts",
                column: "ThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Threads_ThreadId",
                table: "Posts",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCattegories_Cattegories_CattegoryId",
                table: "SubCattegories",
                column: "CattegoryId",
                principalTable: "Cattegories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_AspNetUsers_AuthorId",
                table: "Threads",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_SubCattegories_SubCattegoryId",
                table: "Threads",
                column: "SubCattegoryId",
                principalTable: "SubCattegories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_AuthorId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Threads_ThreadId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCattegories_Cattegories_CattegoryId",
                table: "SubCattegories");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_AspNetUsers_AuthorId",
                table: "Threads");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_SubCattegories_SubCattegoryId",
                table: "Threads");

            migrationBuilder.DropIndex(
                name: "IX_Threads_AuthorId",
                table: "Threads");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ThreadId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ThreadId",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "SubCattegoryId",
                table: "Threads",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Threads",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId1",
                table: "Threads",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Threads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "CattegoryId",
                table: "SubCattegories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId1",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ForumThreadId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LinkValue",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostsCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Threads_AuthorId1",
                table: "Threads",
                column: "AuthorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId1",
                table: "Posts",
                column: "AuthorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ForumThreadId",
                table: "Posts",
                column: "ForumThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_AuthorId1",
                table: "Posts",
                column: "AuthorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Threads_ForumThreadId",
                table: "Posts",
                column: "ForumThreadId",
                principalTable: "Threads",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCattegories_Cattegories_CattegoryId",
                table: "SubCattegories",
                column: "CattegoryId",
                principalTable: "Cattegories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_AspNetUsers_AuthorId1",
                table: "Threads",
                column: "AuthorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_SubCattegories_SubCattegoryId",
                table: "Threads",
                column: "SubCattegoryId",
                principalTable: "SubCattegories",
                principalColumn: "Id");
        }
    }
}
