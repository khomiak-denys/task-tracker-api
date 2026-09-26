using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
#pragma warning disable CA1861 // Prefer 'static readonly' fields over constant array arguments

namespace Users.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a1111111-1111-1111-1111-111111111111"), "a1111111-1111-1111-1111-111111111111", "Admin", "ADMIN" },
                    { new Guid("b2222222-2222-2222-2222-222222222222"), "b2222222-2222-2222-2222-222222222222", "Manager", "MANAGER" },
                    { new Guid("c3333333-3333-3333-3333-333333333333"), "c3333333-3333-3333-3333-333333333333", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b2222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"));
        }
    }
}
