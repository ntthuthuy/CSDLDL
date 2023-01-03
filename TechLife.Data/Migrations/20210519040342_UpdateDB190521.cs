using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateDB190521 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 19, 11, 3, 40, 949, DateTimeKind.Local).AddTicks(9541),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 79, DateTimeKind.Local).AddTicks(7790));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 19, 11, 3, 41, 96, DateTimeKind.Local).AddTicks(4400),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 202, DateTimeKind.Local).AddTicks(421));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 19, 11, 3, 41, 100, DateTimeKind.Local).AddTicks(3266),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 205, DateTimeKind.Local).AddTicks(9510));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 19, 11, 3, 40, 957, DateTimeKind.Local).AddTicks(4816),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 83, DateTimeKind.Local).AddTicks(8772));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 19, 11, 3, 41, 77, DateTimeKind.Local).AddTicks(6776),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 183, DateTimeKind.Local).AddTicks(4753));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ThoiDiemBatDauKinhDoanh",
                table: "HoSo",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayQuyetDinh",
                table: "HoSo",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayHetHan",
                table: "HoSo",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCapPhep",
                table: "HoSo",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCVDatChuan",
                table: "HoSo",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 79, DateTimeKind.Local).AddTicks(7790),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 19, 11, 3, 40, 949, DateTimeKind.Local).AddTicks(9541));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCaiDat",
                table: "ThietBi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 202, DateTimeKind.Local).AddTicks(421),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 19, 11, 3, 41, 96, DateTimeKind.Local).AddTicks(4400));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 205, DateTimeKind.Local).AddTicks(9510),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 19, 11, 3, 41, 100, DateTimeKind.Local).AddTicks(3266));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 83, DateTimeKind.Local).AddTicks(8772),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 19, 11, 3, 40, 957, DateTimeKind.Local).AddTicks(4816));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 15, 9, 22, 55, 183, DateTimeKind.Local).AddTicks(4753),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 5, 19, 11, 3, 41, 77, DateTimeKind.Local).AddTicks(6776));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ThoiDiemBatDauKinhDoanh",
                table: "HoSo",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayQuyetDinh",
                table: "HoSo",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayHetHan",
                table: "HoSo",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCapPhep",
                table: "HoSo",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCVDatChuan",
                table: "HoSo",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "58935647-0f6b-4cd7-a33d-2859d4bd28c1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0acfa4b9-831d-4a12-8cde-7c3959aca6a6", "AQAAAAEAACcQAAAAEKWjgHuPf0iPgjVpOItLp60J3I7KgSo3TdI91WDwSt5uzVDnDtWoipxrbP9EtYkgSQ==" });
        }
    }
}
