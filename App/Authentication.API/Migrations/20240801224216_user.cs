using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.API.Migrations
{
    /// <inheritdoc />
    public partial class user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Biography",
                table: "AspNetUsers",
                newName: "Address");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZipCode",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("68dfdd6b-a20b-498c-ad1d-b0eb12ae076a"),
                column: "ConcurrencyStamp",
                value: "b8c34c29-204e-45cc-81bc-b65deee47e17");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9c1bd465-4ff6-437e-b43e-1f848fd26d99"),
                column: "ConcurrencyStamp",
                value: "8aa8ccd8-7d80-4329-9a63-9afc94cb2993");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cad1bc04-44bf-44b7-ade1-4d6b3fffcba7"),
                column: "ConcurrencyStamp",
                value: "0812e39c-a8cf-4f7a-bc80-16794b2e9f90");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("31feb16f-8c01-4e22-9a6f-df79b7e5582a"),
                columns: new[] { "Address", "CityId", "ConcurrencyStamp", "DistrictId", "PasswordHash", "SecurityStamp", "ZipCode" },
                values: new object[] { null, null, "fe6d97ed-d10d-4b31-85e5-529ec6521683", null, "AQAAAAIAAYagAAAAEBvZdO9KWEDBxo7pFjwBo+8S2c8P9zSzRlrVUbTOUS5n2SofMIbM/ShXr5z7zYd8ZA==", "05f71581-a4da-42b7-80c6-3c488c734db3", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3402ec4-ab10-4f89-bd4a-9d95b601316f"),
                columns: new[] { "Address", "CityId", "ConcurrencyStamp", "DistrictId", "PasswordHash", "SecurityStamp", "ZipCode" },
                values: new object[] { null, null, "cb391149-27f2-4813-8a30-ce47b8deff6e", null, "AQAAAAIAAYagAAAAEBaDUkd2LPGNzgTMFx+nLyat68Z0JZL2JdXPxZpX0Pj9vEUnzU8XV15IokDxbjvGxw==", "2706d550-a89f-4ea3-8488-e19edde57e7b", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "AspNetUsers",
                newName: "Biography");

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
                columns: new[] { "Biography", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "Biography 2", "c4e5a949-e09e-4701-bc7e-a9ee71975e5e", "AQAAAAIAAYagAAAAECRA7U2joLjrnsOqZNP9EHfszP5WCuQZVZILYpMe3DK8UXyPg0wSL0f6iOzTDHhssw==", "a12f246d-6a79-4ed5-8f04-d0f912367121" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3402ec4-ab10-4f89-bd4a-9d95b601316f"),
                columns: new[] { "Biography", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "Biography 1", "58687878-73f6-4dbf-bc14-ab153fccea59", "AQAAAAIAAYagAAAAEMi37pwH5Kl5aTioudBNel6F5X+sOJ+vU9mEi3kFhY3QDMpY77zhi1oV4/I+60PvAA==", "4d0d52a8-6dcd-4008-9f4d-1a318d5083b2" });
        }
    }
}
