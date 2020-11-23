using Microsoft.EntityFrameworkCore.Migrations;

namespace FastMarketRESTAPI.Migrations
{
    public partial class added_LineaCredito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "766bb29c-975f-43c6-8910-7006bc142285");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90e93c86-d67c-4141-9ef5-4a682b16ef44");

            migrationBuilder.AddColumn<double>(
                name: "LineaConsumida",
                table: "Cliente",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LineaCredito",
                table: "Cliente",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "089c5835-ebbc-4074-a5e6-1056102fd313", "3a83d193-06c1-4f27-935d-10ed045188ba", "ADMIN", "ADMIN" },
                    { "0c8fa738-4ef7-4955-9102-4f75d4f33aa7", "f0d2b105-82be-40e4-9225-4ec6ae063584", "USER", "USER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "089c5835-ebbc-4074-a5e6-1056102fd313");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c8fa738-4ef7-4955-9102-4f75d4f33aa7");

            migrationBuilder.DropColumn(
                name: "LineaConsumida",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "LineaCredito",
                table: "Cliente");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "90e93c86-d67c-4141-9ef5-4a682b16ef44", "e302bcbe-1707-4f4d-8525-7ba81942178b", "ADMIN", "ADMIN" },
                    { "766bb29c-975f-43c6-8910-7006bc142285", "1e1c01e7-b09c-413a-abc6-018cf66e66fd", "USER", "USER" }
                });
        }
    }
}
