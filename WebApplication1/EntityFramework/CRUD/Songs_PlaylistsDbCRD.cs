using ClassLibrary1;
using Microsoft.EntityFrameworkCore;
using WebApplication1.EntityFramework.Tables;

namespace WebApplication1.EntityFramework.CRUD;

public class Songs_PlaylistsDbCRD
{
    private void Create(string songGuidStr, string playlistGuidStr)
    {
        var songGuid = GuidChecker.CheckGuid(songGuidStr);
        var playlistGuid = GuidChecker.CheckGuid(playlistGuidStr);

        var songs_playlists = new DbSongs_Playlists
        {
            Id = Guid.NewGuid()
        };
        
        using (var dbContext = new MusicDbContext())
        {
            dbContext.Database.Migrate();
            if (dbContext.Songs.FirstOrDefault(dbS => dbS.Id == songGuid) != null &&
                dbContext.Playlists.FirstOrDefault(dbP => dbP.Id == playlistGuid) != null)
            {
                songs_playlists.SongId = songGuid;
                songs_playlists.PlaylistId = playlistGuid;
                dbContext.Songs_Playlists.Add(songs_playlists);
                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Введён несуществуюший Id песни или плейлиста, попробуйте снова.");
            }
        }
    }

    public void Read()
    {
        using (var dbContext = new MusicDbContext())
        {
            dbContext.Database.Migrate();
            foreach (var db in dbContext.Songs_Playlists)
                Console.WriteLine($"Id: {db.Id}" +
                                  $"SongId: {db.SongId}; " +
                                  $"PlaylistId: {db.PlaylistId}");
        }
    }
    
    private void Delete(string guidStr)
    {
        var guid = GuidChecker.CheckGuid(guidStr);
        using (var dbContext = new MusicDbContext())
        {
            dbContext.Database.Migrate();
            var playlistToDelete = dbContext.Songs.ToList().Find(dbS => dbS.Id == guid);
            if (playlistToDelete != null)
            {
                dbContext.Songs.Remove(playlistToDelete);
                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Введён несуществуюший Id песни, попробуйте снова.");
            }
        }
    }
}