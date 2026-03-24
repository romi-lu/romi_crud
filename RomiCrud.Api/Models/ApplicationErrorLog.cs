namespace RomiCrud.Api.Models;

public class ApplicationErrorLog
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? StackTrace { get; set; }
    public string? RequestPath { get; set; }
    public string? RequestMethod { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
