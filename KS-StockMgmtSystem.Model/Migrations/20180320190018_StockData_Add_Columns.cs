using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KS_StockMgmtSystem.Model.Migrations
{
    public partial class StockData_Add_Columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "StockData",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "CreateUser",
                table: "StockData",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "StockData",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "StockData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "StockData",
                maxLength: 16,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "StockData");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StockData");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "StockData");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "StockData");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "StockData",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
