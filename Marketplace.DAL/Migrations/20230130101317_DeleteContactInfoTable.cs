using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketplaceDAL.Migrations
{
    /// <inheritdoc />
    public partial class DeleteContactInfoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInformations");

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

            migrationBuilder.InsertData(
                table: "PurchasedAdvertisements",
                columns: new[] { "Id", "AdvertisementId", "PurchaseDate", "PurchasedByUserId" },
                values: new object[] { 3, 3, new DateTime(2023, 1, 30, 12, 13, 16, 901, DateTimeKind.Local).AddTicks(5318), 1 });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PurchasedAdvertisements",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "ContactInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInformations", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "PurchasedAdvertisements",
                keyColumn: "Id",
                keyValue: 1,
                column: "PurchaseDate",
                value: new DateTime(2023, 1, 30, 11, 54, 9, 813, DateTimeKind.Local).AddTicks(4677));

            migrationBuilder.UpdateData(
                table: "PurchasedAdvertisements",
                keyColumn: "Id",
                keyValue: 2,
                column: "PurchaseDate",
                value: new DateTime(2023, 1, 30, 11, 54, 9, 813, DateTimeKind.Local).AddTicks(4687));

            migrationBuilder.UpdateData(
                table: "UsersInfo",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 30, 11, 54, 9, 813, DateTimeKind.Local).AddTicks(3617));

            migrationBuilder.UpdateData(
                table: "UsersInfo",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 30, 11, 54, 9, 813, DateTimeKind.Local).AddTicks(3679));

            migrationBuilder.UpdateData(
                table: "UsersInfo",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 1, 30, 11, 54, 9, 813, DateTimeKind.Local).AddTicks(3683));
        }
    }
}
