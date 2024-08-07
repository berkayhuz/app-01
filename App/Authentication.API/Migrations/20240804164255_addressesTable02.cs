using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.API.Migrations
{
    /// <inheritdoc />
    public partial class addressesTable02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
