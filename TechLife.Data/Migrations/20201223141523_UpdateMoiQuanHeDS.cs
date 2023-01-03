using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateMoiQuanHeDS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 23, 21, 15, 22, 382, DateTimeKind.Local).AddTicks(3075),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 23, 15, 51, 11, 309, DateTimeKind.Local).AddTicks(8548));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 23, 21, 15, 22, 384, DateTimeKind.Local).AddTicks(9765),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 23, 15, 51, 11, 311, DateTimeKind.Local).AddTicks(6986));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "ea93eac8-71d9-4ebe-ae3d-f8e2ef6eafaf");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2a40d382-c36c-4802-ab0d-185e6b83dcd3", "AQAAAAEAACcQAAAAEO90LqUHIGVz1kz+kVGkD4I9neZSaEgzvGMvoJJBlCpcfV7ucuvm2YyLss2bMXWXzQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_TrinhDoHoSo_TrinhDoId",
                table: "TrinhDoHoSo",
                column: "TrinhDoId");

            migrationBuilder.CreateIndex(
                name: "IX_TienNghiHoSo_TienNghiId",
                table: "TienNghiHoSo",
                column: "TienNghiId");

            migrationBuilder.CreateIndex(
                name: "IX_NgoaiNguHoSo_NgoaiNguId",
                table: "NgoaiNguHoSo",
                column: "NgoaiNguId");

            migrationBuilder.CreateIndex(
                name: "IX_MucDoTTNNHoSo_MucDoId",
                table: "MucDoTTNNHoSo",
                column: "MucDoId");

            migrationBuilder.CreateIndex(
                name: "IX_LoaiPhongHoSo_LoaiGiuongId",
                table: "LoaiPhongHoSo",
                column: "LoaiGiuongId");

            migrationBuilder.CreateIndex(
                name: "IX_LoaiPhongHoSo_LoaiPhongId",
                table: "LoaiPhongHoSo",
                column: "LoaiPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_DichVuHoSo_DichVuId",
                table: "DichVuHoSo",
                column: "DichVuId");

            migrationBuilder.CreateIndex(
                name: "IX_DichVuHoSo_HoSoId",
                table: "DichVuHoSo",
                column: "HoSoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DichVuHoSo_DichVu_DichVuId",
                table: "DichVuHoSo",
                column: "DichVuId",
                principalTable: "DichVu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DichVuHoSo_HoSo_HoSoId",
                table: "DichVuHoSo",
                column: "HoSoId",
                principalTable: "HoSo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoaiPhongHoSo_LoaiGiuong_LoaiGiuongId",
                table: "LoaiPhongHoSo",
                column: "LoaiGiuongId",
                principalTable: "LoaiGiuong",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoaiPhongHoSo_LoaiPhong_LoaiPhongId",
                table: "LoaiPhongHoSo",
                column: "LoaiPhongId",
                principalTable: "LoaiPhong",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MucDoTTNNHoSo_MucDoThongThaoNgoaiNgu_MucDoId",
                table: "MucDoTTNNHoSo",
                column: "MucDoId",
                principalTable: "MucDoThongThaoNgoaiNgu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NgoaiNguHoSo_NgoaiNgu_NgoaiNguId",
                table: "NgoaiNguHoSo",
                column: "NgoaiNguId",
                principalTable: "NgoaiNgu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TienNghiHoSo_TienNghi_TienNghiId",
                table: "TienNghiHoSo",
                column: "TienNghiId",
                principalTable: "TienNghi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrinhDoHoSo_TrinhDo_TrinhDoId",
                table: "TrinhDoHoSo",
                column: "TrinhDoId",
                principalTable: "TrinhDo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DichVuHoSo_DichVu_DichVuId",
                table: "DichVuHoSo");

            migrationBuilder.DropForeignKey(
                name: "FK_DichVuHoSo_HoSo_HoSoId",
                table: "DichVuHoSo");

            migrationBuilder.DropForeignKey(
                name: "FK_LoaiPhongHoSo_LoaiGiuong_LoaiGiuongId",
                table: "LoaiPhongHoSo");

            migrationBuilder.DropForeignKey(
                name: "FK_LoaiPhongHoSo_LoaiPhong_LoaiPhongId",
                table: "LoaiPhongHoSo");

            migrationBuilder.DropForeignKey(
                name: "FK_MucDoTTNNHoSo_MucDoThongThaoNgoaiNgu_MucDoId",
                table: "MucDoTTNNHoSo");

            migrationBuilder.DropForeignKey(
                name: "FK_NgoaiNguHoSo_NgoaiNgu_NgoaiNguId",
                table: "NgoaiNguHoSo");

            migrationBuilder.DropForeignKey(
                name: "FK_TienNghiHoSo_TienNghi_TienNghiId",
                table: "TienNghiHoSo");

            migrationBuilder.DropForeignKey(
                name: "FK_TrinhDoHoSo_TrinhDo_TrinhDoId",
                table: "TrinhDoHoSo");

            migrationBuilder.DropIndex(
                name: "IX_TrinhDoHoSo_TrinhDoId",
                table: "TrinhDoHoSo");

            migrationBuilder.DropIndex(
                name: "IX_TienNghiHoSo_TienNghiId",
                table: "TienNghiHoSo");

            migrationBuilder.DropIndex(
                name: "IX_NgoaiNguHoSo_NgoaiNguId",
                table: "NgoaiNguHoSo");

            migrationBuilder.DropIndex(
                name: "IX_MucDoTTNNHoSo_MucDoId",
                table: "MucDoTTNNHoSo");

            migrationBuilder.DropIndex(
                name: "IX_LoaiPhongHoSo_LoaiGiuongId",
                table: "LoaiPhongHoSo");

            migrationBuilder.DropIndex(
                name: "IX_LoaiPhongHoSo_LoaiPhongId",
                table: "LoaiPhongHoSo");

            migrationBuilder.DropIndex(
                name: "IX_DichVuHoSo_DichVuId",
                table: "DichVuHoSo");

            migrationBuilder.DropIndex(
                name: "IX_DichVuHoSo_HoSoId",
                table: "DichVuHoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 23, 15, 51, 11, 309, DateTimeKind.Local).AddTicks(8548),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 23, 21, 15, 22, 382, DateTimeKind.Local).AddTicks(3075));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 23, 15, 51, 11, 311, DateTimeKind.Local).AddTicks(6986),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 23, 21, 15, 22, 384, DateTimeKind.Local).AddTicks(9765));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "5c75c832-32a6-4d11-82ab-9081b7ccf018");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9310ed9f-4f1b-45c5-a4ef-e8bfe25ea147", "AQAAAAEAACcQAAAAEEHqnFi7gDCQIWwk+q8TPymxBO6n++utT0smGus5bi7UknaKosLobhi/riHksEWAag==" });
        }
    }
}
