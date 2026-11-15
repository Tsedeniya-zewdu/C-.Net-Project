using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAfterContextChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks");

            migrationBuilder.RenameTable(
                name: "Stocks",
                newName: "Stoke");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Stoke",
                newName: "StokeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stoke",
                table: "Stoke",
                column: "StokeId");

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StokeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Stoke_StokeId",
                        column: x => x.StokeId,
                        principalTable: "Stoke",
                        principalColumn: "StokeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_StokeId",
                table: "Comment",
                column: "StokeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stoke",
                table: "Stoke");

            migrationBuilder.RenameTable(
                name: "Stoke",
                newName: "Stocks");

            migrationBuilder.RenameColumn(
                name: "StokeId",
                table: "Stocks",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks",
                column: "Id");
        }
    }
}
