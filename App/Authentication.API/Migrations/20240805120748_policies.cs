using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.API.Migrations
{
    /// <inheritdoc />
    public partial class policies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EMessage",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ExpressConsentText",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "KvkkPolicy",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Newsteller",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("68dfdd6b-a20b-498c-ad1d-b0eb12ae076a"),
                column: "ConcurrencyStamp",
                value: "b47e1031-623f-4ce7-a398-e059ad9f8766");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9c1bd465-4ff6-437e-b43e-1f848fd26d99"),
                column: "ConcurrencyStamp",
                value: "c06c8fce-cacf-4a3f-8c64-00aaaaa4cf2b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cad1bc04-44bf-44b7-ade1-4d6b3fffcba7"),
                column: "ConcurrencyStamp",
                value: "912d69af-0d5b-4b03-8108-97eca2eafeba");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("31feb16f-8c01-4e22-9a6f-df79b7e5582a"),
                columns: new[] { "BirthDate", "ConcurrencyStamp", "EMessage", "ExpressConsentText", "Gender", "KvkkPolicy", "Newsteller", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "a93077ab-b6be-4090-bcd7-2a1b88bc2706", true, true, 0, true, true, null, "AQAAAAIAAYagAAAAEJRLBK1WKg/MD5JKUnMvtDhJbtDalyUCPpk+uxPLjx+AJ9AoeatBVoKj/14AVnpQBQ==", "a1275096-bbd6-4f2f-be05-2645626bc874" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3402ec4-ab10-4f89-bd4a-9d95b601316f"),
                columns: new[] { "BirthDate", "ConcurrencyStamp", "EMessage", "ExpressConsentText", "Gender", "KvkkPolicy", "Newsteller", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "58fcf2a6-1ae9-45de-9c8f-55d82a4379b8", true, true, 0, true, true, null, "HUZBERKAY@ICLOUD.COM", "AQAAAAIAAYagAAAAEG9TIRJSt8cI+YmcepCnArKTTiKWeoq6TMLCqKKuXvRt/RRa+MLXRIih+w08p7Pzng==", "fa6dbb04-f784-4d2e-be0c-0ef299a59532" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EMessage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ExpressConsentText",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "KvkkPolicy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Newsteller",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("68dfdd6b-a20b-498c-ad1d-b0eb12ae076a"),
                column: "ConcurrencyStamp",
                value: "8b7519a8-213b-43c4-b694-32126e38d76d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9c1bd465-4ff6-437e-b43e-1f848fd26d99"),
                column: "ConcurrencyStamp",
                value: "0470b0ec-1576-40c5-88c3-e0ef14e92d70");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cad1bc04-44bf-44b7-ade1-4d6b3fffcba7"),
                column: "ConcurrencyStamp",
                value: "1cfb4ab2-08d6-4381-9a8f-1b69ec46d7b4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("31feb16f-8c01-4e22-9a6f-df79b7e5582a"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29dc82af-1f50-4a1a-9dd7-b1993430d949", "DENEME@ICLOUD.COM", "AQAAAAIAAYagAAAAEFDe8AgxZW5zabKEM4GLpyO1gMtj0PGJW08ijFz5nR5HmDdShTqyXgT8kVjq/XcHgg==", "45583ca3-0e25-4f30-8fcb-f30c78856828" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3402ec4-ab10-4f89-bd4a-9d95b601316f"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b9c38b40-2593-42ec-8bf8-a2c55fe8afa4", "HUZBERKAY@ICLOUD.COM", "huzberkay@icloud.com", "AQAAAAIAAYagAAAAEP0xfUG2Nh6DmSiiHE6WWj0Dj5q6qJcuhNQBVLbXX/K3IiUlyEj/2/wnGYdM4uMwsA==", "209db2bd-0687-4bf6-97b8-ed036a9c4240" });
        }
    }
}
