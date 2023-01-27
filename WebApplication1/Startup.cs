using MassTransit;
using WebApplication1.Consumers;
using WebApplication1.Consumers.AlbumsConsumers;

namespace WebApplication1;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // Формируем список используемых служб/сервисов
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        // services.AddAutoMapper(typeof(AppMappingProfile));
        
        services.AddMassTransit(mt =>
        {
            mt.AddConsumer<PostUserConsumer>();
            mt.AddConsumer<CreateAlbumConsumer>();
            mt.AddConsumer<GetAlbumConsumer>();
            mt.AddConsumer<UpdateAlbumConsumer>();
            mt.AddConsumer<DeleteAlbumConsumer>();
            
            mt.UsingRabbitMq((context, config) =>
            {
                config.Host("localhost", "/", host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });
                config.ReceiveEndpoint("postUserKey", ep => ep.ConfigureConsumer<PostUserConsumer>(context));
                config.ReceiveEndpoint("create", ep => ep.ConfigureConsumer<CreateAlbumConsumer>(context));
                config.ReceiveEndpoint("get", ep => ep.ConfigureConsumer<GetAlbumConsumer>(context));
                config.ReceiveEndpoint("update", ep => ep.ConfigureConsumer<UpdateAlbumConsumer>(context));
                config.ReceiveEndpoint("delete", ep => ep.ConfigureConsumer<DeleteAlbumConsumer>(context));
            });
        });
        services.AddMassTransitHostedService();
    }

    public void Configure(IApplicationBuilder app)
    {
        // Чтобы запрос пришёл куда надо
        app.UseRouting();

        // Настройка HTTP запроса 
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}