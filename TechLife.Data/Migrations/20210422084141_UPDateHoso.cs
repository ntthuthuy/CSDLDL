using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UPDateHoso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HanhTrinh_HoSo_NoiDenId",
                table: "HanhTrinh");

            migrationBuilder.DropIndex(
                name: "IX_HanhTrinh_NoiDenId",
                table: "HanhTrinh");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 41, 40, 447, DateTimeKind.Local).AddTicks(2807),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 21, 56, 137, DateTimeKind.Local).AddTicks(2574));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 41, 40, 450, DateTimeKind.Local).AddTicks(7708),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 21, 56, 139, DateTimeKind.Local).AddTicks(5805));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 41, 40, 543, DateTimeKind.Local).AddTicks(6072),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 21, 56, 224, DateTimeKind.Local).AddTicks(9641));

            migrationBuilder.AddColumn<int>(
                name: "HoSoId",
                table: "HanhTrinh",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "1a2034ab-374e-4a16-8281-afa5e900ee54");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "62fa62d2-4d10-409c-9e13-9148fe1ac7ff", "AQAAAAEAACcQAAAAEJf70wsyOPqfXMoNqx0ltqV/21jdtMRUE2ZZjEJtSqJGrMRz0ilFj74WCobSGRulMQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_HanhTrinh_HoSoId",
                table: "HanhTrinh",
                column: "HoSoId");

            migrationBuilder.AddForeignKey(
                name: "FK_HanhTrinh_HoSo_HoSoId",
                table: "HanhTrinh",
                column: "HoSoId",
                principalTable: "HoSo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HanhTrinh_HoSo_HoSoId",
                table: "HanhTrinh");

            migrationBuilder.DropIndex(
                name: "IX_HanhTrinh_HoSoId",
                table: "HanhTrinh");

            migrationBuilder.DropColumn(
                name: "HoSoId",
                table: "HanhTrinh");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 21, 56, 137, DateTimeKind.Local).AddTicks(2574),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 41, 40, 447, DateTimeKind.Local).AddTicks(2807));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 21, 56, 139, DateTimeKind.Local).AddTicks(5805),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 41, 40, 450, DateTimeKind.Local).AddTicks(7708));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoSoThanhTra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 22, 15, 21, 56, 224, DateTimeKind.Local).AddTicks(9641),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 4, 22, 15, 41, 40, 543, DateTimeKind.Local).AddTicks(6072));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "49818eb7-5407-408b-90a5-2b36a3a3c1e6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3578df24-3b93-4b85-a28c-20c3b6871f28", "AQAAAAEAACcQAAAAEHNYehjDvw11d7i668fJ1AFci0eN5Q1YNlkGm2Qq18NuPoIpZY5hBkKye1dfexQ7vg==" });

            migrationBuilder.CreateIndex(
                name: "IX_HanhTrinh_NoiDenId",
                table: "HanhTrinh",
                column: "NoiDenId");

            migrationBuilder.AddForeignKey(
                name: "FK_HanhTrinh_HoSo_NoiDenId",
                table: "HanhTrinh",
                column: "NoiDenId",
                principalTable: "HoSo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
