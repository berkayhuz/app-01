using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("68dfdd6b-a20b-498c-ad1d-b0eb12ae076a"),
                column: "ConcurrencyStamp",
                value: "88523ab7-18a1-4da5-943c-0895d90d8ce3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9c1bd465-4ff6-437e-b43e-1f848fd26d99"),
                column: "ConcurrencyStamp",
                value: "63f7503e-8c63-4ce1-bd61-73a6d490c0aa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cad1bc04-44bf-44b7-ade1-4d6b3fffcba7"),
                column: "ConcurrencyStamp",
                value: "86ee0f99-35ae-41c3-8f5d-536ff68c0c16");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("31feb16f-8c01-4e22-9a6f-df79b7e5582a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b0299f7-8501-4ce9-9278-21a78c39b35e", "AQAAAAIAAYagAAAAEDnGzBBKrwiWgW62Bp+DUtlV65UEQ45Gu8M9Atb8+O8HolOLBjfFioLowK+6LAOSnw==", "9604f997-1ed0-4f9f-8516-a196f05df589" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3402ec4-ab10-4f89-bd4a-9d95b601316f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "05047c55-35f3-4a93-bc35-dd488a9e2a92", "AQAAAAIAAYagAAAAEAp3EUCwuFHffB6RhhfjHtEV/Qvh6VmLXr1LaUbUZesfW8/n8+zCuO86T50AYqEyWQ==", "fb8dfbef-9b8f-44c3-91e7-c72568b5b2f6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a93077ab-b6be-4090-bcd7-2a1b88bc2706", "AQAAAAIAAYagAAAAEJRLBK1WKg/MD5JKUnMvtDhJbtDalyUCPpk+uxPLjx+AJ9AoeatBVoKj/14AVnpQBQ==", "a1275096-bbd6-4f2f-be05-2645626bc874" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3402ec4-ab10-4f89-bd4a-9d95b601316f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58fcf2a6-1ae9-45de-9c8f-55d82a4379b8", "AQAAAAIAAYagAAAAEG9TIRJSt8cI+YmcepCnArKTTiKWeoq6TMLCqKKuXvRt/RRa+MLXRIih+w08p7Pzng==", "fa6dbb04-f784-4d2e-be0c-0ef299a59532" });
        }
    }
}
