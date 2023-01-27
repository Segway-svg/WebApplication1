using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.EntityFramework.Tables;

public class DbSongs_Playlists
{
    public Guid Id { get; set; }
    public Guid PlaylistId { get; set; }
    public DbPlaylists Playlist { get; set; }
    public Guid SongId { get; set; }
    public DbSongs Song { get; set; }
}

public class DbSongs_PlaylistsConfiguration : IEntityTypeConfiguration<DbSongs_Playlists>
{
    public void Configure(EntityTypeBuilder<DbSongs_Playlists> builder)
    {
        builder
            .ToTable("Songs_Playlists");
        builder
            .HasKey(l => l.Id);

        builder
            .HasOne(s => s.Song)
            .WithMany(sp => sp.Songs_Playlists)
            .HasForeignKey(si => si.SongId);

        builder
            .HasOne(s => s.Playlist)
            .WithMany(sp => sp.Songs_Playlists)
            .HasForeignKey(si => si.PlaylistId);
    }
}