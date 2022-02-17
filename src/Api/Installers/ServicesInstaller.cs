namespace Messages.Api.Installers;
using Apis;
using Apis.Interfaces;
using Data;

public class ServicesInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddScoped<IMessageService, MessageService>()
            .AddTransient<IApi, MessageApi>();
    }
}
