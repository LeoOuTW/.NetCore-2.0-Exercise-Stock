using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KS_StockMgmtSystem.Model.Migrations
{
    public partial class VersionData_Add_Columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "VersionData",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "CreateUser",
                table: "VersionData",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "VersionData",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "VersionData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUser",
                table: "VersionData",
                maxLength: 16,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateUser",
                table: "VersionData");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "VersionData");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "VersionData");

            migrationBuilder.DropColumn(
                name: "UpdateUser",
                table: "VersionData");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "VersionData",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
