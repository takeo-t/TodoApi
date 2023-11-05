namespace TodoApi.Models;

public class TodoItem
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public TodoStatus Status { get; set; }
}