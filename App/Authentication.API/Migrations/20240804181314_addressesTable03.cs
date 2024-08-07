using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.API.Migrations
{
    /// <inheritdoc />
    public partial class addressesTable03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TaxNumber",
                table: "Addresses",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TaxAdministration",
                table: "Addresses",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TCNumber",
                table: "Addresses",
                type: "char(11)",
                fixedLength: true,
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "PassaportNumber",
                table: "Addresses",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "PassaportCountry",
                table: "Addresses",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CommercialTitle",
                table: "Addresses",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29dc82af-1f50-4a1a-9dd7-b1993430d949", "AQAAAAIAAYagAAAAEFDe8AgxZW5zabKEM4GLpyO1gMtj0PGJW08ijFz5nR5HmDdShTqyXgT8kVjq/XcHgg==", "45583ca3-0e25-4f30-8fcb-f30c78856828" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3402ec4-ab10-4f89-bd4a-9d95b601316f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b9c38b40-2593-42ec-8bf8-a2c55fe8afa4", "AQAAAAIAAYagAAAAEP0xfUG2Nh6DmSiiHE6WWj0Dj5q6qJcuhNQBVLbXX/K3IiUlyEj/2/wnGYdM4uMwsA==", "209db2bd-0687-4bf6-97b8-ed036a9c4240" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TaxNumber",
                table: "Addresses",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TaxAdministration",
                table: "Addresses",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "TCNumber",
                table: "Addresses",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(11)",
                oldFixedLength: true,
                oldMaxLength: 11,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "PassaportNumber",
                table: "Addresses",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "PassaportCountry",
                table: "Addresses",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CommercialTitle",
                table: "Addresses",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("68dfdd6b-a20b-498c-ad1d-b0eb12ae076a"),
                column: "ConcurrencyStamp",
                value: "03d6d1f8-1f48-47f5-910d-263f77d08aaa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9c1bd465-4ff6-437e-b43e-1f848fd26d99"),
                column: "ConcurrencyStamp",
                value: "999e6116-fcc9-44da-9b2e-d96f3729d9ad");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cad1bc04-44bf-44b7-ade1-4d6b3fffcba7"),
                column: "ConcurrencyStamp",
                value: "086cacc0-6558-48d7-9c00-a102455b9f75");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("31feb16f-8c01-4e22-9a6f-df79b7e5582a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bdb6ed7b-785f-441d-9a5c-646301bb5b19", "AQAAAAIAAYagAAAAEJLP76dSSSJDN2UCD0g7xvCHJqpd/NL3aJrfzioImpYLQeouXeTDWK3VtCeaZCjhdQ==", "f9e78bfd-421a-4d30-9c64-e97ddecb8cad" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3402ec4-ab10-4f89-bd4a-9d95b601316f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e16107c-9a23-4c41-9cfc-5ff14452cfc0", "AQAAAAIAAYagAAAAEGrvySDT29uMROkWXk9Bs5lGXg96BP+ltpGZEPW1CpPRB+vzCnOVkyPNR/rpwAOqjQ==", "5644cc20-7bf7-49aa-b207-25d536d54b69" });
        }
    }
}
