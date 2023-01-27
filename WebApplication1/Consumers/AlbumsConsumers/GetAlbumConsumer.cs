using ClientServer.Requests.AlbumsRequests;
using ClientServer.Responses.AlbumsResponses;
using MassTransit;
using WebApplication1.EntityFramework.CRUD;
using WebApplication1.Mappers.AlbumsMappers;

namespace WebApplication1.Consumers.AlbumsConsumers;

public class GetAlbumConsumer : IConsumer<GetAlbumRequest>
{
    public async Task Consume(ConsumeContext<GetAlbumRequest> context)
    {
        GetAlbumResponse response = new GetAlbumResponse();
        
        AlbumsDbCRUD albumsDbCrud = new AlbumsDbCRUD();
        
        var albumToRead = await albumsDbCrud.GetAlbumAsync(context.Message.Id);

        if (albumToRead == null)
        {
            response.StatusCode = false;
            response.Errors = new List<string>();
            response.Errors.Add("There isn't such album");
            await context.RespondAsync(response);
        }

        GetAlbumMapper getAlbumMapper = new GetAlbumMapper();
        response = getAlbumMapper.Map(albumToRead);
        response.StatusCode = true;
        await context.RespondAsync(response);
    }
}