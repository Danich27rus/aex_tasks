using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AexTest.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ManagerID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Customers_Managers_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "Managers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CustomerID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Даниил" });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "ID", "Name" },
                values: new object[] { 2, "Артём" });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "ID", "Name" },
                values: new object[] { 3, "Илья" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "ID", "ManagerID", "Name" },
                values: new object[] { 1, 2, "Геннадий" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "ID", "ManagerID", "Name" },
                values: new object[] { 2, 3, "Алексей" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "ID", "ManagerID", "Name" },
                values: new object[] { 3, 1, "Иннокентий" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "ID", "Amount", "CustomerID", "Date" },
                values: new object[] { 1, 100m, 1, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "ID", "Amount", "CustomerID", "Date" },
                values: new object[] { 2, 1000m, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "ID", "Amount", "CustomerID", "Date" },
                values: new object[] { 3, 100m, 3, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ManagerID",
                table: "Customers",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Managers");
        }
    }
}
