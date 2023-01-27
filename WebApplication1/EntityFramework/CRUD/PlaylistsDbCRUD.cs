using ClassLibrary1;
using Microsoft.EntityFrameworkCore;
using WebApplication1.EntityFramework.Tables;

namespace WebApplication1.EntityFramework.CRUD;

public class PlaylistsDbCRUD
{
    private void Create(string name)
    {
        var playlist = new DbPlaylists
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        using (var dbContext = new MusicDbContext())
        {
            dbContext.Database.Migrate();
            dbContext.Playlists.Add(playlist);
            dbContext.SaveChanges();
        }
    }

    public void Read()
    {
        using (var dbContext = new MusicDbContext())
        {
            dbContext.Database.Migrate();
            foreach (var db in dbContext.Playlists)
                Console.WriteLine($"Id: {db.Id}; " +
                                  $"Name: {db.Name}; ");
        }
    }
    
    private void Update(string guidStr, string name)
    {
        var guid = GuidChecker.CheckGuid(guidStr);
        
        using (var dbContext = new MusicDbContext())
        {
            dbContext.Database.Migrate();
            var playlistToUpdate = dbContext.Playlists.ToList().Find(dbP => dbP.Id == guid);
            if (playlistToUpdate != null)
            {
                playlistToUpdate.Name = name;
                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Введён несуществуюший Id плейлиста, попробуйте снова.");
            }
        }
    }
    
    private void Delete(string guidStr)
    {
        var guid = GuidChecker.CheckGuid(guidStr);
        
        using (var dbContext = new MusicDbContext())
        {
            dbContext.Database.Migrate();
            var playlistToDelete = dbContext.Playlists.ToList().Find(dbP => dbP.Id == guid);
            var songToDeleteFromPlaylist = dbContext.Songs_Playlists.ToList().Find(dbS => dbS.PlaylistId == guid);
            if (playlistToDelete != null)
            {
                dbContext.Playlists.Remove(playlistToDelete);
                dbContext.Songs_Playlists.Remove(songToDeleteFromPlaylist);
                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Введён несуществуюший Id плейлиста, попробуйте снова.");
            }
        }
    }
}