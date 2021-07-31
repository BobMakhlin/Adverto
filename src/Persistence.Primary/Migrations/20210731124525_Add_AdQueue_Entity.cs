using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Primary.Migrations
{
    public partial class Add_AdQueue_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdQueues",
                columns: table => new
                {
                    AdQueueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentAdIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdQueues", x => x.AdQueueId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdQueues");
        }
    }
}
