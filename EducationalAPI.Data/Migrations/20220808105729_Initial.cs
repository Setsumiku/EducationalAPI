using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalAPI.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountOfMaterials = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "EduMatTypes",
                columns: table => new
                {
                    EduMatTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EduMatTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EduMatTypeDesc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduMatTypes", x => x.EduMatTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserLogin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "EduMatNavpoints",
                columns: table => new
                {
                    EduMatNavpointId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: true),
                    EduMatTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EduMatLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EduMatTypeId = table.Column<int>(type: "int", nullable: true),
                    EduMatTimeCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduMatNavpoints", x => x.EduMatNavpointId);
                    table.ForeignKey(
                        name: "FK_EduMatNavpoints_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId");
                    table.ForeignKey(
                        name: "FK_EduMatNavpoints_EduMatTypes_EduMatTypeId",
                        column: x => x.EduMatTypeId,
                        principalTable: "EduMatTypes",
                        principalColumn: "EduMatTypeId");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewContents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewScore = table.Column<int>(type: "int", nullable: false),
                    EduMatNavpointId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_EduMatNavpoints_EduMatNavpointId",
                        column: x => x.EduMatNavpointId,
                        principalTable: "EduMatNavpoints",
                        principalColumn: "EduMatNavpointId");
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "AmountOfMaterials", "AuthorDesc", "AuthorName" },
                values: new object[,]
                {
                    { 1, null, "Mentor and programmer", null },
                    { 2, null, "Some random guy with a webcam", null },
                    { 3, null, "Mood booster and lifestyle coach", null },
                    { 4, null, "It just works", null }
                });

            migrationBuilder.InsertData(
                table: "EduMatTypes",
                columns: new[] { "EduMatTypeId", "EduMatTypeDesc", "EduMatTypeName" },
                values: new object[,]
                {
                    { 1, "It's a video.", "Video" },
                    { 2, "It's a written article.", "Article" },
                    { 3, "It's a type of knowledge transfer directly into a brain", "DT" },
                    { 4, "Long form audio.", "Podcast" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "EduMatNavpointId", "ReviewContents", "ReviewScore" },
                values: new object[,]
                {
                    { 1, null, "Very nice", 5 },
                    { 2, null, "Very sad", 1 },
                    { 3, null, "Sad", 3 },
                    { 4, null, "Nice", 7 },
                    { 5, null, "Based", 9 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "UserLogin", "UserPassword", "UserRole" },
                values: new object[,]
                {
                    { 1, "admin", "admin", "Admin" },
                    { 2, "user", "user", "User" }
                });

            migrationBuilder.InsertData(
                table: "EduMatNavpoints",
                columns: new[] { "EduMatNavpointId", "AuthorId", "EduMatLocation", "EduMatTimeCreated", "EduMatTitle", "EduMatTypeId" },
                values: new object[,]
                {
                    { 1, 4, "My ssd", new DateTime(2022, 8, 8, 12, 57, 29, 784, DateTimeKind.Local).AddTicks(5864), "Yes man", 2 },
                    { 2, 1, "Web", new DateTime(2022, 8, 8, 12, 57, 29, 784, DateTimeKind.Local).AddTicks(5922), "No man", 2 },
                    { 3, 4, "Web", new DateTime(2022, 8, 8, 12, 57, 29, 784, DateTimeKind.Local).AddTicks(5924), "Cat video", 1 },
                    { 4, 3, "Lost", new DateTime(2022, 8, 8, 12, 57, 29, 784, DateTimeKind.Local).AddTicks(5926), "Dog teaches programming", 3 },
                    { 5, 1, "Stolen", new DateTime(2022, 8, 8, 12, 57, 29, 784, DateTimeKind.Local).AddTicks(5928), "Coincidence", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EduMatNavpoints_AuthorId",
                table: "EduMatNavpoints",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_EduMatNavpoints_EduMatTypeId",
                table: "EduMatNavpoints",
                column: "EduMatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_EduMatNavpointId",
                table: "Reviews",
                column: "EduMatNavpointId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "EduMatNavpoints");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "EduMatTypes");
        }
    }
}
