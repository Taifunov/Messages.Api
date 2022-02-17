namespace Messages.Api.Data;
using Context;
using Models;

public interface IMessageService
{
    Task<List<Message>> GetMessagesAsync();
    Task<List<Message>> GetMessagesAsync(string name);
    Task<Message?> GetMessageAsync(int messageId);
}

public class MessageService : IMessageService
{
    private readonly MessageContext _context;
    public MessageService(MessageContext context)
    {
        _context = context;
    }

    public async Task<List<Message>> GetMessagesAsync() => await _context.Messages.ToListAsync();

    public async Task<List<Message>> GetMessagesAsync(string name) 
        => await _context.Messages.Where(m => m.Name != null && m.Name.Contains(name))
            .ToListAsync();

    public async Task<Message?> GetMessageAsync(int messageId)
        => await _context.Messages.FindAsync(messageId);
}