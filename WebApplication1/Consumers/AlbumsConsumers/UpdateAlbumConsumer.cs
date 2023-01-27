using ClientServer.Requests.AlbumsRequests;
using ClientServer.Responses.AlbumsResponses;
using MassTransit;
using WebApplication1.EntityFramework.CRUD;

namespace WebApplication1.Consumers.AlbumsConsumers;


public class UpdateAlbumConsumer : IConsumer<UpdateAlbumRequest>
{
    public async Task Consume(ConsumeContext<UpdateAlbumRequest> context)
    {
        UpdateAlbumResponse response = new UpdateAlbumResponse();
        
        AlbumsDbCRUD albumsDbCrud = new AlbumsDbCRUD();
        
        var albumToUpdate = await albumsDbCrud.UpdateAlbumAsync(context.Message.Id, context.Message.Name, context.Message.Description);

        if (albumToUpdate == null)
        {
            response.StatusCode = false;
            response.Errors = new List<string>();
            response.Errors.Add("There isn't such album");
            await context.RespondAsync(response);
        }

        response.StatusCode = true;
        
        await context.RespondAsync(response);
    }
}