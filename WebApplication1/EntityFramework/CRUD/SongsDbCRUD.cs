using Microsoft.EntityFrameworkCore;
using WebApplication1.EntityFramework.Tables;

namespace WebApplication1.EntityFramework.CRUD;

public class SongsDbCRUD
{
    private void Create(string name, bool isFamous, Guid guid)
    {
        var song = new DbSongs
        {
            Id = Guid.NewGuid(),
            Name = name,
            IsFamous = isFamous
        };

        using (var dbContext = new MusicDbContext())
        {
            dbContext.Database.Migrate();
            if (dbContext.Albums.FirstOrDefault(dbG => dbG.Id == guid) != null)
            {
                song.AlbumId = guid;
                dbContext.Songs.Add(song);
                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Введён несуществуюший Id группы, попробуйте снова.");
            }
        }
    }

    public void Read()
    {
        using (var dbContext = new MusicDbContext())
        {
            dbContext.Database.Migrate();
            foreach (var db in dbContext.Songs)
                Console.WriteLine($"Id: {db.Id}; " +
                                  $"Name: {db.Name}; " +
                                  $"IsFamous: {db.IsFamous}; " +
                                  $"GroupId: {db.AlbumId}; ");
        }
    }
    
    private void Update(Guid guid, string name, bool isFamous)
    {
        using (var dbContext = new MusicDbContext())
        {
            dbContext.Database.Migrate();
            var songToUpdate = dbContext.Songs.ToList().Find(dbS => dbS.Id == guid);
            if (songToUpdate != null)
            {
                songToUpdate.Name = name;
                songToUpdate.IsFamous = isFamous;
                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Введён несуществуюший Id песни, попробуйте снова.");
            }
        }
    }

    private void Delete(Guid guid)
    {
        using (var dbContext = new MusicDbContext())
        {
            dbContext.Database.Migrate();
            var songToDelete = dbContext.Songs.ToList().Find(dbS => dbS.Id == guid);
            var playlistToDelete = dbContext.Songs_Playlists.ToList().Find(dbS => dbS.SongId == guid);
            if (songToDelete != null)
            {
                dbContext.Songs.Remove(songToDelete);
                dbContext.Songs_Playlists.Remove(playlistToDelete);
                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Введён несуществуюший Id песни, попробуйте снова.");
            }
        }
    }

    private Guid CheckGuid(string guidStr)
    {
        var guid = new Guid();
        
        if (Guid.TryParse(guidStr, out _))
        {
            guid = Guid.Parse(guidStr);
        }
        else
        {
            Console.WriteLine("Неверный формат, попробуйте снова");
        }
        return guid;
    }
}