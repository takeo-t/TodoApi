using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models;

/// <summary>
/// ToDoアイテムを表すクラス。
/// </summary>
public class TodoItem
{
    /// <summary>
    /// ToDoアイテムの一意な識別子。
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// ToDoアイテムのタイトル。最大100文字まで。
    /// </summary>
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// ToDoアイテムの内容。最大100文字まで。
    /// </summary>
    [StringLength(100, ErrorMessage = "Content cannot be longer than 100 characters.")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// ToDoアイテムが作成された日時。
    /// </summary>
    public DateTime DateTime { get; set; }

    /// <summary>
    /// ToDoアイテムの現在のステータス。
    /// </summary>
    public TodoStatus Status { get; set; }

    /// <summary>
    /// ToDoアイテムが完了した日時。nullの場合、未完了を意味する。
    /// </summary>
    public DateTime? CompletedAt { get; set; }
}
