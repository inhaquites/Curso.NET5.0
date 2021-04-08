using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UserPadrao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreateAt", "Email", "Name", "UpdateAt" },
                values: new object[] { new Guid("140a848f-9623-41cc-a394-346ebce19e92"), new DateTime(2021, 4, 1, 14, 45, 27, 653, DateTimeKind.Utc).AddTicks(3757), "user@example.com", "Administrador", new DateTime(2021, 4, 1, 14, 45, 27, 653, DateTimeKind.Utc).AddTicks(4898) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("140a848f-9623-41cc-a394-346ebce19e92"));
        }
    }
}
