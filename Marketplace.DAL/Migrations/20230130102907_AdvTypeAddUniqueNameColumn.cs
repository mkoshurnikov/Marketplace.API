using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketplaceDAL.Migrations
{
    /// <inheritdoc />
    public partial class AdvTypeAddUniqueNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdvTypeName",
                table: "AdvTypes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "PurchasedAdvertisements",
                keyColumn: "Id",
                keyValue: 1,
                column: "PurchaseDate",
                value: new DateTime(2023, 1, 30, 12, 29, 7, 683, DateTimeKind.Local).AddTicks(4941));

            migrationBuilder.UpdateData(
                table: "PurchasedAdvertisements",
                keyColumn: "Id",
                keyValue: 2,
                column: "PurchaseDate",
                value: new DateTime(2023, 1, 30, 12, 29, 7, 683, DateTimeKind.Local).AddTicks(4947));

            migrationBuilder.UpdateData(
                table: "PurchasedAdvertisements",
                keyColumn: "Id",
                keyValue: 3,
                column: "PurchaseDate",
                value: new DateTime(2023, 1, 30, 12, 29, 7, 683, DateTimeKind.Local).AddTicks(4948));

            migrationBuilder.UpdateData(
                table: "UsersInfo",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 30, 12, 29, 7, 683, DateTimeKind.Local).AddTicks(4313));

            migrationBuilder.UpdateData(
                table: "UsersInfo",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 30, 12, 29, 7, 683, DateTimeKind.Local).AddTicks(4359));

            migrationBuilder.UpdateData(
                table: "UsersInfo",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 30, 12, 29, 7, 683, DateTimeKind.Local).AddTicks(4362));

            migrationBuilder.CreateIndex(
                name: "IX_AdvTypes_AdvTypeName",
                table: "AdvTypes",
                column: "AdvTypeName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AdvTypes_AdvTypeName",
                table: "AdvTypes");

            migrationBuilder.AlterColumn<string>(
                name: "AdvTypeName",
                table: "AdvTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "PurchasedAdvertisements",
                keyColumn: "Id",
                keyValue: 1,
                column: "PurchaseDate",
                value: new DateTime(2023, 1, 30, 12, 13, 16, 901, DateTimeKind.Local).AddTicks(5312));

            migrationBuilder.UpdateData(
                table: "PurchasedAdvertisements",
                keyColumn: "Id",
                keyValue: 2,
                column: "PurchaseDate",
                value: new DateTime(2023, 1, 30, 12, 13, 16, 901, DateTimeKind.Local).AddTicks(5316));

            migrationBuilder.UpdateData(
                table: "PurchasedAdvertisements",
                keyColumn: "Id",
                keyValue: 3,
                column: "PurchaseDate",
                value: new DateTime(2023, 1, 30, 12, 13, 16, 901, DateTimeKind.Local).AddTicks(5318));

            migrationBuilder.UpdateData(
                table: "UsersInfo",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 30, 12, 13, 16, 901, DateTimeKind.Local).AddTicks(4704));

            migrationBuilder.UpdateData(
                table: "UsersInfo",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 30, 12, 13, 16, 901, DateTimeKind.Local).AddTicks(4745));

            migrationBuilder.UpdateData(
                table: "UsersInfo",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 30, 12, 13, 16, 901, DateTimeKind.Local).AddTicks(4748));
        }
    }
}
