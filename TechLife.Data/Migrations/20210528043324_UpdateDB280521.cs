using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateDB280521 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 24, DateTimeKind.Local).AddTicks(4485),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 19, 11, 3, 40, 949, DateTimeKind.Local).AddTicks(9541));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 111, DateTimeKind.Local).AddTicks(188),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 19, 11, 3, 41, 96, DateTimeKind.Local).AddTicks(4400));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 112, DateTimeKind.Local).AddTicks(9048),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 19, 11, 3, 41, 100, DateTimeKind.Local).AddTicks(3266));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 27, DateTimeKind.Local).AddTicks(5058),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 19, 11, 3, 40, 957, DateTimeKind.Local).AddTicks(4816));

            migrationBuilder.AddColumn<string>(
                name: "Loai",
                table: "HoSoVanBan",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 100, DateTimeKind.Local).AddTicks(4061),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 19, 11, 3, 41, 77, DateTimeKind.Local).AddTicks(6776));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "12460d6f-e29b-4310-b190-337b687b4cbc");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "603c17f1-ff5a-4275-870b-a952697d2545", "AQAAAAEAACcQAAAAEGUkdaTE/iwXM2sIA5i6Stj+usX+81yo8D9Z8SD7xf3v/mAR1+4CxNBCArgHcD2WRg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Loai",
                table: "HoSoVanBan");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 19, 11, 3, 40, 949, DateTimeKind.Local).AddTicks(9541),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 24, DateTimeKind.Local).AddTicks(4485));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 19, 11, 3, 41, 96, DateTimeKind.Local).AddTicks(4400),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 111, DateTimeKind.Local).AddTicks(188));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 19, 11, 3, 41, 100, DateTimeKind.Local).AddTicks(3266),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 112, DateTimeKind.Local).AddTicks(9048));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 19, 11, 3, 40, 957, DateTimeKind.Local).AddTicks(4816),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 27, DateTimeKind.Local).AddTicks(5058));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 19, 11, 3, 41, 77, DateTimeKind.Local).AddTicks(6776),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 28, 11, 33, 24, 100, DateTimeKind.Local).AddTicks(4061));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "0a983aea-1bb4-43e6-8975-e6c4e0cc6456");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "85d6e0a7-d149-4d83-bd1d-3c3e253533df", "AQAAAAEAACcQAAAAECkWwe2t6FTRt8hyelK/gUqafOidVD49y2MKVx1rWz6uCTl4fHAd8vaxBzeTyk5FiA==" });
        }
    }
}
