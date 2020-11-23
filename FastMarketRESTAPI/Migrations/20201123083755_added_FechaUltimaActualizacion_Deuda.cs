using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FastMarketRESTAPI.Migrations
{
    public partial class added_FechaUltimaActualizacion_Deuda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "089c5835-ebbc-4074-a5e6-1056102fd313");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c8fa738-4ef7-4955-9102-4f75d4f33aa7");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaUltimaActualizacion",
                table: "Deuda",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "447e2017-fe7a-4b79-8718-8eae3f5f06ca", "82b24558-2322-4dc1-9e00-e7af78b2b383", "ADMIN", "ADMIN" },
                    { "390da1b8-dd5a-47eb-bfc9-83ece5d5ca3c", "a020d89e-4751-4f80-a966-f9f0f6359676", "USER", "USER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "390da1b8-dd5a-47eb-bfc9-83ece5d5ca3c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "447e2017-fe7a-4b79-8718-8eae3f5f06ca");

            migrationBuilder.DropColumn(
                name: "FechaUltimaActualizacion",
                table: "Deuda");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "089c5835-ebbc-4074-a5e6-1056102fd313", "3a83d193-06c1-4f27-935d-10ed045188ba", "ADMIN", "ADMIN" },
                    { "0c8fa738-4ef7-4955-9102-4f75d4f33aa7", "f0d2b105-82be-40e4-9225-4ec6ae063584", "USER", "USER" }
                });
        }
    }
}
