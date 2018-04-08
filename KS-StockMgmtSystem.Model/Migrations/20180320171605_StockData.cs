using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KS_StockMgmtSystem.Model.Migrations
{
    public partial class StockData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConfirmMonth = table.Column<int>(nullable: false),
                    ConfirmYear = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    StockNum = table.Column<int>(nullable: false),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockData");
        }
    }
}
