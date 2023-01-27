using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApplication1.EntityFramework;


[DbContext(typeof(MusicDbContext))]
[Migration("20230111172700_InitialCreate")]
public class InitialCreate : Migration {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Albums",
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false, type: "uniqueidentifier"),
                Name = table.Column<string>(nullable: false),
                Description = table.Column<string>(nullable: true),
                ReleaseDate = table.Column<DateTime>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Albums", l => l.Id);
            }
        );

        migrationBuilder.CreateTable(
            name: "Songs",
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false),
                Name = table.Column<string>(nullable: true),
                IsFamous = table.Column<bool>(nullable: false),
                AlbumId = table.Column<Guid>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Songs", l => l.Id);
            }
        );
        
        migrationBuilder.AddForeignKey(
            name: "FK_Albums_Songs_AlbumId",
            table: "Songs",
            column: "AlbumId",
            principalTable: "Albums",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
    
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("Albums");
        migrationBuilder.DropTable("Songs");
    }
}