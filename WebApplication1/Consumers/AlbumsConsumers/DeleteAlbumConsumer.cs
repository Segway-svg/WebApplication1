using ClientServer.Requests.AlbumsRequests;
using ClientServer.Responses.AlbumsResponses;
using MassTransit;
using WebApplication1.EntityFramework.CRUD;

namespace WebApplication1.Consumers.AlbumsConsumers;


public class DeleteAlbumConsumer : IConsumer<DeleteAlbumRequest>
{
    public async Task Consume(ConsumeContext<DeleteAlbumRequest> context)
    {
        DeleteAlbumResponse response = new DeleteAlbumResponse();
        
        AlbumsDbCRUD albumsDbCrud = new AlbumsDbCRUD();
        
        var albumToDelete = await albumsDbCrud.DeleteAlbumAsync(context.Message.Id);
        
        if (albumToDelete == null)
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