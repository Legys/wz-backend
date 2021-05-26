﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace wz_backend.Migrations
{
    public partial class RemoveAdminsSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsAdmin", "Nickname", "Password" },
                values: new object[] { 1L, true, "WzBeats", "12345" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsAdmin", "Nickname", "Password" },
                values: new object[] { 2L, true, "Legys", "12345" });
        }
    }
}
