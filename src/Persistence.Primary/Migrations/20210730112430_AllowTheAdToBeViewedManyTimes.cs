using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Primary.Migrations
{
    public partial class AllowTheAdToBeViewedManyTimes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ViewedAds");

            migrationBuilder.CreateTable(
                name: "AdViews",
                columns: table => new
                {
                    AdViewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ViewedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdViews", x => x.AdViewId);
                    table.ForeignKey(
                        name: "FK_AdViews_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "AdId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdViews_AdId",
                table: "AdViews",
                column: "AdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdViews");

            migrationBuilder.CreateTable(
                name: "ViewedAds",
                columns: table => new
                {
                    AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ViewedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewedAds", x => x.AdId);
                    table.ForeignKey(
                        name: "FK_ViewedAds_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "AdId",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
