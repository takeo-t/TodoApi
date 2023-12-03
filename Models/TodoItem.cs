using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models;

public class TodoItem
{
    public long Id { get; set; }
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
    public string Title { get; set; } = string.Empty;
    [StringLength(100, ErrorMessage = "Content cannot be longer than 100 characters.")]
    public string Content { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public TodoStatus Status { get; set; }
    public DateTime? CompletedAt { get; set; }
}