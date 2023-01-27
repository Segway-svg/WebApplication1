using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using WebApplication1.EntityFramework;

namespace DigitalOfficeHW2.EntityFramework;

[DbContext(typeof(MusicDbContext))]
[Migration("20230111192900_AddManyToMany")]
public class AddManyToMany : Migration {
    
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Playlists",
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false, type: "uniqueidentifier"),
                Name = table.Column<string>(nullable: false, type: "nvarchar(max)")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Playlists", l => l.Id);
            }
        );
                
        migrationBuilder.CreateTable(
            name: "Songs_Playlists",
            columns: table => new
            {
                Id = table.Column<Guid>(nullable: false, type: "uniqueidentifier"),
                SongId = table.Column<Guid>(nullable: false, type: "uniqueidentifier"),
                PlaylistId = table.Column<Guid>(nullable: false, type: "uniqueidentifier"),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Songs_Playlistss", l => l.Id);
                table.ForeignKey(
                    name: "FK_Songs_Playlists_SongId",
                    column: l => l.SongId,
                    principalTable: "Songs",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Songs_Playlists_PlaylistId",
                    column: l => l.PlaylistId,
                    principalTable: "Playlists",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });
    }
            
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("Songs");
        migrationBuilder.DropTable("Songs_Playlists");
    }
}