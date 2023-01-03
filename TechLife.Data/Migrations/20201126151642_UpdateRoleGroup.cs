using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class UpdateRoleGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Roles_RolesId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_RolesId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "RolesId",
                table: "Groups");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 11, 26, 22, 16, 42, 161, DateTimeKind.Local).AddTicks(805));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "1a017d7e-7379-4d8a-ac60-cc7bf27baaf7");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "824c4a7f-7257-4563-a688-99f98e9b8c3e", "AQAAAAEAACcQAAAAEM1h2jdd0akzahkII1Om9la0ILsM2C87xuAdq4q9y8ZLOO0hlyXmucr/TVbfN8NBxQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RolesId",
                table: "Groups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 11, 26, 22, 10, 21, 893, DateTimeKind.Local).AddTicks(6756));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "89b1155b-9f80-42f1-a5a7-c77137f1c728");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cd8e2e95-10d6-4d17-b863-77b8b11bf9bc", "AQAAAAEAACcQAAAAECoCh3DB/QHWKWMW0a/BsS8K80p6JF4lTowhQnRk0wlLpR9tpAAgeT5FoCRLrvbEZw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_RolesId",
                table: "Groups",
                column: "RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Roles_RolesId",
                table: "Groups",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
