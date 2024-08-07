using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.API.Migrations
{
    /// <inheritdoc />
    public partial class addressesTable01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommercialTitle",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "InvoicePayer",
                table: "Addresses",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceType",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PassaportCountry",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PassaportNumber",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TCNumber",
                table: "Addresses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxAdministration",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TaxNumber",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("68dfdd6b-a20b-498c-ad1d-b0eb12ae076a"),
                column: "ConcurrencyStamp",
                value: "7dabf548-a77c-470c-b7d3-39ecc507e82a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9c1bd465-4ff6-437e-b43e-1f848fd26d99"),
                column: "ConcurrencyStamp",
                value: "570c237e-a47b-436b-b319-03c5353c13ed");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cad1bc04-44bf-44b7-ade1-4d6b3fffcba7"),
                column: "ConcurrencyStamp",
                value: "d2c95ced-5f35-4e34-b842-a6a0958d2882");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("31feb16f-8c01-4e22-9a6f-df79b7e5582a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "79b71ea4-a7d8-45f9-babd-d36831962aa3", "AQAAAAIAAYagAAAAEMI8pPTEA3t7uXZAFkjPmKE1PFw48jaV+tAkleBCUYKE+xUAeN948FiZOySkaDW8Qg==", "cb9ea503-44c8-4e25-8766-248e7ef9c0dc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3402ec4-ab10-4f89-bd4a-9d95b601316f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80250a3c-1089-457e-9096-f6e413a5b441", "AQAAAAIAAYagAAAAEPu3r7yWea1vpU6VKhvNsRLPJoUhL7RFa+jbbF/q2xohvDq639vNp8NnBRAJQ0E45g==", "1f8c6827-79ca-4016-b606-06f32d934219" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommercialTitle",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "InvoicePayer",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "InvoiceType",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "PassaportCountry",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "PassaportNumber",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "TCNumber",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "TaxAdministration",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "TaxNumber",
                table: "Addresses");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("68dfdd6b-a20b-498c-ad1d-b0eb12ae076a"),
                column: "ConcurrencyStamp",
                value: "efbf37a9-a0d4-4526-8d5a-8c8f723e71f1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9c1bd465-4ff6-437e-b43e-1f848fd26d99"),
                column: "ConcurrencyStamp",
                value: "52316ecc-1d5e-48e4-b263-faadf811e6a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cad1bc04-44bf-44b7-ade1-4d6b3fffcba7"),
                column: "ConcurrencyStamp",
                value: "751a9d37-9c04-4cd8-8548-dc3efc84c86c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("31feb16f-8c01-4e22-9a6f-df79b7e5582a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99c55d6a-7160-48cf-b90d-9f28f7f5c70e", "AQAAAAIAAYagAAAAEAX2eoFZylDleijTAptQkWjZ25F/7gmySBCnvu5xc6wTIXYbPlcwz8MW6ima6CZYug==", "a9ab1574-6afd-435c-b55d-fe82f439acc1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3402ec4-ab10-4f89-bd4a-9d95b601316f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e096a7e5-2062-4378-a46b-0432bb6ebf20", "AQAAAAIAAYagAAAAEL8lLsnr2lKTS1vkMDtixDhyVu0I2ko7NmRK4SiVIpWyaEVzn0y+nbsewTWh3JwY2w==", "e0f20a16-1d8f-4b02-8b15-6e713da792cb" });
        }
    }
}
