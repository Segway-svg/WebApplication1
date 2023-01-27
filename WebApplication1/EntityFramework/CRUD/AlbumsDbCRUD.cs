using Microsoft.EntityFrameworkCore;
using WebApplication1.EntityFramework.Tables;

namespace WebApplication1.EntityFramework.CRUD;

public class AlbumsDbCRUD
{
    public async Task<Guid> CreateAlbumAsync(DbAlbums album)
    {
        album.Id = new Guid();
        album.ReleaseDate = DateTime.Now;

        using (var dbContext = new MusicDbContext())
        {
            dbContext.Database.Migrate();
            dbContext.Albums.Add(album);
            await dbContext.SaveChangesAsync();
        }

        return album.Id;
    }

    public async Task<DbAlbums> GetAlbumAsync(Guid guid)
    {
        using (var dbContext = new MusicDbContext())
        {
            dbContext.Database.Migrate();
            var albumToShow = await dbContext.Albums.AsNoTracking().Where(dbA => dbA.Id == guid).FirstOrDefaultAsync();
            return albumToShow;
        }
    }

    public async Task<DbAlbums> UpdateAlbumAsync(Guid guid, string name, string description)
    {
        using (var dbContext = new MusicDbContext())
        {
            dbContext.Database.Migrate();
            var albumToUpdate = dbContext.Albums.ToList().Find(dbA => dbA.Id == guid);
            if (albumToUpdate != null)
            {
                albumToUpdate.Name = name;
                albumToUpdate.Description = description;
                dbContext.SaveChanges();
            }
            return albumToUpdate;
        }
    }
    
    public async Task<DbAlbums> DeleteAlbumAsync(Guid guid)
    {
        using (var dbContext = new MusicDbContext())
        {
            dbContext.Database.Migrate();
            var albumToDelete = dbContext.Albums.ToList().Find(dbA => dbA.Id == guid);
            if (albumToDelete != null)
            {
                var songsToDelete = dbContext.Songs.Where(x => x.AlbumId == albumToDelete.Id);
                foreach (var songToDelete in songsToDelete) dbContext.Songs.Remove(songToDelete);
                dbContext.Albums.Remove(albumToDelete);
                dbContext.SaveChanges();
            }

            return albumToDelete;
        }
    }
}