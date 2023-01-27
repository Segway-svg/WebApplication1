using ClientServer.Requests.AlbumsRequests;
using WebApplication1.EntityFramework.Tables;

namespace WebApplication1.Mappers.AlbumsMappers;

public class CreateAlbumMapper
{
    public DbAlbums Map(CreateAlbumRequest albumRequest)
    {
        return new DbAlbums()
        {
            Name = albumRequest.Name,
            Description = albumRequest.Description
        };
    }
}