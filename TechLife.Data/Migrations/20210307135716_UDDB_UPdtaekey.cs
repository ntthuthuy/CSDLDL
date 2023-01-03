using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UDDB_UPdtaekey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoPhanHoSo_HoSo_HoSoId",
                table: "BoPhanHoSo");

            migrationBuilder.DropForeignKey(
                name: "FK_DichVuHoSo_HoSo_HoSoId",
                table: "DichVuHoSo");

            migrationBuilder.DropForeignKey(
                name: "FK_LoaiPhongHoSo_HoSo_HoSoId",
                table: "LoaiPhongHoSo");

            migrationBuilder.DropForeignKey(
                name: "FK_MucDoTTNNHoSo_HoSo_HoSoId",
                table: "MucDoTTNNHoSo");

            migrationBuilder.DropForeignKey(
                name: "FK_NgoaiNguHoSo_HoSo_HoSoId",
                table: "NgoaiNguHoSo");

            migrationBuilder.DropForeignKey(
                name: "FK_ThucDonHoSo_HoSo_HosoId",
                table: "ThucDonHoSo");

            migrationBuilder.DropForeignKey(
                name: "FK_TienNghiHoSo_HoSo_HoSoId",
                table: "TienNghiHoSo");

            migrationBuilder.DropForeignKey(
                name: "FK_TrinhDoHoSo_HoSo_HoSoId",
                table: "TrinhDoHoSo");

            migrationBuilder.DropForeignKey(
                name: "FK_VeDichVuHoSo_HoSo_HosoId",
                table: "VeDichVuHoSo");

            migrationBuilder.DropIndex(
                name: "IX_VeDichVuHoSo_HosoId",
                table: "VeDichVuHoSo");

            migrationBuilder.DropIndex(
                name: "IX_TrinhDoHoSo_HoSoId",
                table: "TrinhDoHoSo");

            migrationBuilder.DropIndex(
                name: "IX_TienNghiHoSo_HoSoId",
                table: "TienNghiHoSo");

            migrationBuilder.DropIndex(
                name: "IX_ThucDonHoSo_HosoId",
                table: "ThucDonHoSo");

            migrationBuilder.DropIndex(
                name: "IX_NgoaiNguHoSo_HoSoId",
                table: "NgoaiNguHoSo");

            migrationBuilder.DropIndex(
                name: "IX_MucDoTTNNHoSo_HoSoId",
                table: "MucDoTTNNHoSo");

            migrationBuilder.DropIndex(
                name: "IX_LoaiPhongHoSo_HoSoId",
                table: "LoaiPhongHoSo");

            migrationBuilder.DropIndex(
                name: "IX_DichVuHoSo_HoSoId",
                table: "DichVuHoSo");

            migrationBuilder.DropIndex(
                name: "IX_BoPhanHoSo_HoSoId",
                table: "BoPhanHoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 7, 20, 57, 14, 942, DateTimeKind.Local).AddTicks(2547),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 4, 10, 20, 37, 804, DateTimeKind.Local).AddTicks(4390));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 7, 20, 57, 14, 946, DateTimeKind.Local).AddTicks(6814),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 3, 4, 10, 20, 37, 807, DateTimeKind.Local).AddTicks(3215));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "77a1f9bb-d6cd-4b0e-b797-1705a7fefb69");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1ff31325-82b2-4e46-a18a-7c90f540ced7", "AQAAAAEAACcQAAAAEK6r7vlqQvcf5+xyZyVx7G3lFdHeoj+MEdF5AZQLGrdL1jASQ1ScQmwRt1m/VfMu8Q==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 4, 10, 20, 37, 804, DateTimeKind.Local).AddTicks(4390),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 7, 20, 57, 14, 942, DateTimeKind.Local).AddTicks(2547));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 3, 4, 10, 20, 37, 807, DateTimeKind.Local).AddTicks(3215),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 3, 7, 20, 57, 14, 946, DateTimeKind.Local).AddTicks(6814));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "ed831f98-1cad-40f6-8899-447a70268ac6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ba5e4499-2981-40f1-bf75-41fcdbdd5c38", "AQAAAAEAACcQAAAAEJedIvsKHfQaklV1OsddICsPK082vJ3mnYKJLxN25hrq0uVL1oTlvZpkuPJSuM8uWQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_VeDichVuHoSo_HosoId",
                table: "VeDichVuHoSo",
                column: "HosoId");

            migrationBuilder.CreateIndex(
                name: "IX_TrinhDoHoSo_HoSoId",
                table: "TrinhDoHoSo",
                column: "HoSoId");

            migrationBuilder.CreateIndex(
                name: "IX_TienNghiHoSo_HoSoId",
                table: "TienNghiHoSo",
                column: "HoSoId");

            migrationBuilder.CreateIndex(
                name: "IX_ThucDonHoSo_HosoId",
                table: "ThucDonHoSo",
                column: "HosoId");

            migrationBuilder.CreateIndex(
                name: "IX_NgoaiNguHoSo_HoSoId",
                table: "NgoaiNguHoSo",
                column: "HoSoId");

            migrationBuilder.CreateIndex(
                name: "IX_MucDoTTNNHoSo_HoSoId",
                table: "MucDoTTNNHoSo",
                column: "HoSoId");

            migrationBuilder.CreateIndex(
                name: "IX_LoaiPhongHoSo_HoSoId",
                table: "LoaiPhongHoSo",
                column: "HoSoId");

            migrationBuilder.CreateIndex(
                name: "IX_DichVuHoSo_HoSoId",
                table: "DichVuHoSo",
                column: "HoSoId");

            migrationBuilder.CreateIndex(
                name: "IX_BoPhanHoSo_HoSoId",
                table: "BoPhanHoSo",
                column: "HoSoId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoPhanHoSo_HoSo_HoSoId",
                table: "BoPhanHoSo",
                column: "HoSoId",
                principalTable: "HoSo",
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
                name: "FK_LoaiPhongHoSo_HoSo_HoSoId",
                table: "LoaiPhongHoSo",
                column: "HoSoId",
                principalTable: "HoSo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MucDoTTNNHoSo_HoSo_HoSoId",
                table: "MucDoTTNNHoSo",
                column: "HoSoId",
                principalTable: "HoSo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NgoaiNguHoSo_HoSo_HoSoId",
                table: "NgoaiNguHoSo",
                column: "HoSoId",
                principalTable: "HoSo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ThucDonHoSo_HoSo_HosoId",
                table: "ThucDonHoSo",
                column: "HosoId",
                principalTable: "HoSo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TienNghiHoSo_HoSo_HoSoId",
                table: "TienNghiHoSo",
                column: "HoSoId",
                principalTable: "HoSo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrinhDoHoSo_HoSo_HoSoId",
                table: "TrinhDoHoSo",
                column: "HoSoId",
                principalTable: "HoSo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VeDichVuHoSo_HoSo_HosoId",
                table: "VeDichVuHoSo",
                column: "HosoId",
                principalTable: "HoSo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
