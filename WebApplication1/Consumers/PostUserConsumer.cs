// using ClientServer.Requests;
// using ClientServer.Responses;
// using MassTransit;
//
// namespace WebApplication1.Consumers;
//
// public class PostUserConsumer : IConsumer<PostUserRequest>
// {
//     public Task Consume(ConsumeContext<PostUserRequest> context)
//     {
//         PostUserResponse response = new PostUserResponse();
//         response.IsSuccess = true;
//         
//         return context.RespondAsync(response);
//     }
// }
using ClientServer.Requests;
using ClientServer.Responses;
using MassTransit;

namespace WebApplication1.Consumers;

public class PostUserConsumer : IConsumer<PostUserRequest>
{
    public Task Consume(ConsumeContext<PostUserRequest> context)
    {
        PostUserResponse response = new PostUserResponse();
        response.Name = context.Message.UserName;
        response.IsSuccess = true;
        return context.RespondAsync(response);
    }
}