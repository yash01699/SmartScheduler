using Dapper;
namespace SmartScheduler.Models;


[Table("ToDoItem")]

public record TodoItem
{
    public TodoItem() { }

    public TodoItem(int id, string name, bool isComplete, int priority, int time)
    {
        Id = id;
        Name = name;
        IsComplete = isComplete;
        Priority = priority;
        Time = time;
    }

    [Key]
    [Column("Id")]
    public int Id { get; init; }
    [Column("Name")]
    public string? Name { get; init; }
    [Column("IsComplete")]
    public bool IsComplete { get; init; }
    [Column("Time")]
    public int Time { get; init; }
    [Column("Priority")]
    public int Priority { get; init; }
}