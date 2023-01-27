using Microsoft.EntityFrameworkCore;
using WebApplication1.EntityFramework.Tables;

namespace WebApplication1.EntityFramework;

public class MusicDbContext : DbContext
{
    public DbSet<DbAlbums> Albums { get; set; }
    public DbSet<DbSongs> Songs { get; set; }
    public DbSet<DbPlaylists> Playlists { get; set; }
    public DbSet<DbSongs_Playlists> Songs_Playlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=localhost\sqlexpress;Database=Mp3DB;Trusted_Connection=True;TrustServerCertificate=True");
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<DbSongs_Playlists>()
    //         .HasOne(s => s.Song)
    //         .WithMany(sp => sp.Songs_Playlists)
    //         .HasForeignKey(si => si.SongId);
    //
    //     modelBuilder.Entity<DbSongs_Playlists>()
    //         .HasOne(s => s.Playlist)
    //         .WithMany(sp => sp.Songs_Playlists)
    //         .HasForeignKey(si => si.PlaylistId);
    // }
}