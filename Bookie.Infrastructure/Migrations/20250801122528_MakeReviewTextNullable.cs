using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bookie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeReviewTextNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Reviews",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Reviews",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("88888888-8888-8888-8888-888888888888"), "Horror" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "User" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Publisher" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Bio", "Email", "PasswordHash", "RoleId", "Username", "WebsiteUrl" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), "I am the admin", "admin@bookie.com", "$2a$12$4B7JV2SARmlaWaPbE.rwp.av/sype46qlFOt5Jy2g4EXZpuzmrsGi", new Guid("33333333-3333-3333-3333-333333333333"), "admin", null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "I love books", "reader1@bookie.com", "$2a$12$4B7JV2SARmlaWaPbE.rwp.av/sype46qlFOt5Jy2g4EXZpuzmrsGi", new Guid("11111111-1111-1111-1111-111111111111"), "reader1", null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Avid book collector", "reader2@bookie.com", "$2a$12$4B7JV2SARmlaWaPbE.rwp.av/sype46qlFOt5Jy2g4EXZpuzmrsGi", new Guid("11111111-1111-1111-1111-111111111111"), "reader2", null },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "I publish books", "publisher@bookie.com", "$2a$12$4B7JV2SARmlaWaPbE.rwp.av/sype46qlFOt5Jy2g4EXZpuzmrsGi", new Guid("22222222-2222-2222-2222-222222222222"), "pub1", null }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "AverageRating", "Blurb", "CoverPhotoUrl", "CreatedAt", "CreatedByUserId", "GenreId", "GetBookUrl", "ISBN", "Language", "PageCount", "PublicationYear", "Title" },
                values: new object[] { new Guid("99999999-9999-9999-9999-999999999999"), "Mary Shelley", 4.5, "A classic tale of science and horror.", "https://ia600100.us.archive.org/view_archive.php?archive=/5/items/l_covers_0012/l_covers_0012_75.zip&file=0012752093-L.jpg", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("77777777-7777-7777-7777-777777777777"), new Guid("88888888-8888-8888-8888-888888888888"), "https://www.gutenberg.org/files/84/84-h/84-h.htm", "9780143131847", "English", 280, 1818, "Frankenstein" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookId", "CreatedAt", "Rating", "Text", "UserId" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, "An amazing read!", new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Very well written, but a bit slow in the middle.", new Guid("66666666-6666-6666-6666-666666666666") }
                });
        }
    }
}
