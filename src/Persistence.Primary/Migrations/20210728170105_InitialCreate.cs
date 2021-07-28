using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Primary.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(3600)", maxLength: 3600, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.AdId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "DisabledAds",
                columns: table => new
                {
                    AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisabledAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisabledAds", x => x.AdId);
                    table.ForeignKey(
                        name: "FK_DisabledAds_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "AdId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "AdCategory",
                columns: table => new
                {
                    AdsAdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriesCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdCategory", x => new { x.AdsAdId, x.CategoriesCategoryId });
                    table.ForeignKey(
                        name: "FK_AdCategory_Ads_AdsAdId",
                        column: x => x.AdsAdId,
                        principalTable: "Ads",
                        principalColumn: "AdId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdCategory_Categories_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdTag",
                columns: table => new
                {
                    AdsAdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsTagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdTag", x => new { x.AdsAdId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_AdTag_Ads_AdsAdId",
                        column: x => x.AdsAdId,
                        principalTable: "Ads",
                        principalColumn: "AdId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdTag_Tags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdCategory_CategoriesCategoryId",
                table: "AdCategory",
                column: "CategoriesCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AdTag_TagsTagId",
                table: "AdTag",
                column: "TagsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Title",
                table: "Categories",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Title",
                table: "Tags",
                column: "Title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdCategory");

            migrationBuilder.DropTable(
                name: "AdTag");

            migrationBuilder.DropTable(
                name: "DisabledAds");

            migrationBuilder.DropTable(
                name: "ViewedAds");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Ads");
        }
    }
}
