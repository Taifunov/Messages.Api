namespace Messages.Api.Apis;
using Interfaces;
using Models;
using Data;

public class MessageApi : IApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/messages", Get)
            .Produces<List<Message>>()
            .WithName("GetAllMessages")
            .WithTags("Getters");

        app.MapGet("/messages/{id}", GetById)
            .Produces<Message>()
            .WithName("GetMessage")
            .WithTags("Getters");
    }
    
    private async Task<IResult> Get(IMessageService messageService) 
        => Results.Ok(await messageService.GetMessagesAsync());

    private async Task<IResult> GetById(int id, IMessageService messageService) =>
        await messageService.GetMessageAsync(id) is Message message
            ? Results.Ok(message)
            : Results.NotFound();
}