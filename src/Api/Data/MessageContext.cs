namespace Messages.Api.Context;
using Models;

public class MessageContext : DbContext
{
    public MessageContext(DbContextOptions<MessageContext> options) : base(options) { }
    public DbSet<Message> Messages => Set<Message>();
}