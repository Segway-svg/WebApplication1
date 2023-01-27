namespace WebApplication1.EntityFramework.Tables;

public class DbPlaylists
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<DbSongs_Playlists> Songs_Playlists { get; set; } = new List<DbSongs_Playlists>();
}