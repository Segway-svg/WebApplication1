using ClientServer.Requests.AlbumsRequests;
using ClientServer.Responses.AlbumsResponses;
using MassTransit;
using WebApplication1.EntityFramework.CRUD;
using WebApplication1.Mappers.AlbumsMappers;

namespace WebApplication1.Consumers.AlbumsConsumers;


public class CreateAlbumConsumer : IConsumer<CreateAlbumRequest>
{
    public async Task Consume(ConsumeContext<CreateAlbumRequest> context)
    {
        CreateAlbumResponse response = new CreateAlbumResponse();
        
        AlbumsDbCRUD albumsDbCrud = new AlbumsDbCRUD();
        CreateAlbumMapper albumMapper = new CreateAlbumMapper();

        response.Id = await albumsDbCrud.CreateAlbumAsync(albumMapper.Map(context.Message));
        response.StatusCode = true;
        
        await context.RespondAsync(response);
    }
}