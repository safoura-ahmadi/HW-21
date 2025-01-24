using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarCheckup.Infra.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(20)", fixedLength: true, maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "varchar(6)", fixedLength: true, maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plate = table.Column<string>(type: "nvarchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    GenerationYear = table.Column<DateOnly>(type: "date", nullable: false),
                    OwnerMeliCode = table.Column<string>(type: "varchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    OwnerMobile = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    Company = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarModels_ModelId",
                        column: x => x.ModelId,
                        principalTable: "CarModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CheckupRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeToDone = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckupRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckupRequests_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RejectedCheckupRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RejectedCheckupRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RejectedCheckupRequests_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarModels",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "پژو206" },
                    { 2, "سمند" },
                    { 3, "پراید" }
                });

            migrationBuilder.InsertData(
                table: "Operators",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { 1, "123456", "operator" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Company", "GenerationYear", "ModelId", "OwnerMeliCode", "OwnerMobile", "Plate" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2020, 1, 1), 2, "0312889161", "09302675549", "33و283-68" },
                    { 2, 2, new DateOnly(2025, 1, 1), 3, "0322879161", "09303685549", "99خ902-33" },
                    { 3, 1, new DateOnly(2015, 1, 1), 1, "0312889161", "09302675549", "45ر444-41" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ModelId",
                table: "Cars",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckupRequests_CarId",
                table: "CheckupRequests",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectedCheckupRequests_CarId",
                table: "RejectedCheckupRequests",
                column: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckupRequests");

            migrationBuilder.DropTable(
                name: "Operators");

            migrationBuilder.DropTable(
                name: "RejectedCheckupRequests");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "CarModels");
        }
    }
}
