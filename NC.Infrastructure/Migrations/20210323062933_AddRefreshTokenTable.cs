using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NC.Infrastructure.Migrations
{
    public partial class AddRefreshTokenTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InsertDate = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<string>(maxLength: 255, nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    UpdateUserId = table.Column<string>(maxLength: 255, nullable: true),
                    TokenHashed = table.Column<string>(maxLength: 50, nullable: false),
                    Expires = table.Column<DateTime>(nullable: false),
                    UserAgent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_AspNetUsers_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_InsertUserId",
                table: "RefreshToken",
                column: "InsertUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");
        }
    }
}
