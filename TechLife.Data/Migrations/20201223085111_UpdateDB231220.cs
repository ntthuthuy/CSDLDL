using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateDB231220 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 23, 15, 51, 11, 309, DateTimeKind.Local).AddTicks(8548),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 23, 14, 39, 18, 474, DateTimeKind.Local).AddTicks(5407));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 23, 15, 51, 11, 311, DateTimeKind.Local).AddTicks(6986),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 12, 23, 14, 39, 18, 477, DateTimeKind.Local).AddTicks(3853));

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
                name: "IX_BoPhanHoSo_BoPhanId",
                table: "BoPhanHoSo",
                column: "BoPhanId");

            migrationBuilder.CreateIndex(
                name: "IX_BoPhanHoSo_HoSoId",
                table: "BoPhanHoSo",
                column: "HoSoId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoPhanHoSo_BoPhan_BoPhanId",
                table: "BoPhanHoSo",
                column: "BoPhanId",
                principalTable: "BoPhan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoPhanHoSo_HoSo_HoSoId",
                table: "BoPhanHoSo",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoPhanHoSo_BoPhan_BoPhanId",
                table: "BoPhanHoSo");

            migrationBuilder.DropForeignKey(
                name: "FK_BoPhanHoSo_HoSo_HoSoId",
                table: "BoPhanHoSo");

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
                name: "IX_BoPhanHoSo_BoPhanId",
                table: "BoPhanHoSo");

            migrationBuilder.DropIndex(
                name: "IX_BoPhanHoSo_HoSoId",
                table: "BoPhanHoSo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Trackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 23, 14, 39, 18, 474, DateTimeKind.Local).AddTicks(5407),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 23, 15, 51, 11, 309, DateTimeKind.Local).AddTicks(8548));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 12, 23, 14, 39, 18, 477, DateTimeKind.Local).AddTicks(3853),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 12, 23, 15, 51, 11, 311, DateTimeKind.Local).AddTicks(6986));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "1efb5baa-d567-449c-b664-f29bfb3c6741");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "eec534f7-9d81-4bba-baee-4b8bbcd4aaf0", "AQAAAAEAACcQAAAAENpoNEsLwIWfaip3hKTd0vMMTyiEPhbAqXI+qvk+G6OnNjcdf3jNEikoMiK+MDvO6w==" });
        }
    }
}
