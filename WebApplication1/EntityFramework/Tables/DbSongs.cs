using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.EntityFramework.Tables;

public class DbSongs
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsFamous { get; set; }
    public Guid AlbumId { get; set; }
    public DbAlbums Album { get; set; }
    public ICollection<DbSongs_Playlists> Songs_Playlists { get; set; } = new List<DbSongs_Playlists>();
}
