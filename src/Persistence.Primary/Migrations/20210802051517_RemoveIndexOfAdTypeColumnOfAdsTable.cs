using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Primary.Migrations
{
    public partial class RemoveIndexOfAdTypeColumnOfAdsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ads_AdType",
                table: "Ads");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Ads_AdType",
                table: "Ads",
                column: "AdType");
        }
    }
}
