using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechLife.Data.Migrations
{
    public partial class spThongKeCSLT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[spThongKeCSLT]
                    @value nvarchar(50)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    select Ten from HoSo
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"ALTER PROCEDURE [dbo].[spThongKeCSLT]
                    @value nvarchar(50)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    select Ten from HoSo where IsDelete = 0
                END";

            migrationBuilder.Sql(sp);
        }
    }
}
