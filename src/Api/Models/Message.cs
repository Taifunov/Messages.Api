namespace Messages.Api.Models;

public class Message
{
    public int Id { get; set; }
    public ulong GuildId { get; set; }
    public string? Name { get; set; }
    public string? Text { get; set; }
    public string? EmbedJson { get; set; }
    public string? ImageUrl { get; set; }
}