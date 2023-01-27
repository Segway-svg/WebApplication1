using ClientServer.Responses.AlbumsResponses;
using WebApplication1.EntityFramework.Tables;

namespace WebApplication1.Mappers.AlbumsMappers;

public class GetAlbumMapper
{
    public GetAlbumResponse Map(DbAlbums album)
    {
        return new GetAlbumResponse()
        {
            Id = album.Id,
            Name = album.Name,
            Description = album.Description,
            ReleaseDate = album.ReleaseDate
        };
    }
}