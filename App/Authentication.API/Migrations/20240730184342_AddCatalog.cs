using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("68dfdd6b-a20b-498c-ad1d-b0eb12ae076a"),
                column: "ConcurrencyStamp",
                value: "f6bac5dd-ca8a-4b8c-a0e2-d6a3eeeed06c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9c1bd465-4ff6-437e-b43e-1f848fd26d99"),
                column: "ConcurrencyStamp",
                value: "4cbf7da5-49dd-4462-8781-765017b3383d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cad1bc04-44bf-44b7-ade1-4d6b3fffcba7"),
                column: "ConcurrencyStamp",
                value: "e3194b6e-458b-46c0-9abc-703c022e041a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("31feb16f-8c01-4e22-9a6f-df79b7e5582a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c4e5a949-e09e-4701-bc7e-a9ee71975e5e", "AQAAAAIAAYagAAAAECRA7U2joLjrnsOqZNP9EHfszP5WCuQZVZILYpMe3DK8UXyPg0wSL0f6iOzTDHhssw==", "a12f246d-6a79-4ed5-8f04-d0f912367121" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3402ec4-ab10-4f89-bd4a-9d95b601316f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58687878-73f6-4dbf-bc14-ab153fccea59", "AQAAAAIAAYagAAAAEMi37pwH5Kl5aTioudBNel6F5X+sOJ+vU9mEi3kFhY3QDMpY77zhi1oV4/I+60PvAA==", "4d0d52a8-6dcd-4008-9f4d-1a318d5083b2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("68dfdd6b-a20b-498c-ad1d-b0eb12ae076a"),
                column: "ConcurrencyStamp",
                value: "41baba6c-0eb1-4a12-90a3-9aab1f765b17");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9c1bd465-4ff6-437e-b43e-1f848fd26d99"),
                column: "ConcurrencyStamp",
                value: "db806f9c-1908-48ae-847d-d2730fd99914");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cad1bc04-44bf-44b7-ade1-4d6b3fffcba7"),
                column: "ConcurrencyStamp",
                value: "4e290c4d-b32e-4db5-aec0-4a6fec538536");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("31feb16f-8c01-4e22-9a6f-df79b7e5582a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18d26447-8d03-41c6-8103-792b1eded0ac", "AQAAAAIAAYagAAAAEJ+FTHRuzCTKyKHh7fsmXMZ8YlwFtSWN3iQd8Gr/oYC6hqsTrySzg5CUCwMSoOY9kw==", "310c0e9c-091d-4280-bc62-3698941b35cc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3402ec4-ab10-4f89-bd4a-9d95b601316f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07197cd5-f2c4-439b-afa1-f6ead15b5022", "AQAAAAIAAYagAAAAEOTlpXVjH0l+tKWcKggzbgSZFT6i7SCw/pEhXy8TBKdZRnpGRGBoTM8ep9fY/AltJw==", "e3276de7-1fe7-4deb-b00b-8978397b26d4" });
        }
    }
}
