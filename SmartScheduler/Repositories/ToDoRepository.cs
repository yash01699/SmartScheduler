using System.Data;
using Dapper;
using SmartScheduler.Models;

namespace SmartScheduler.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly ILogger<TodoRepository> _logger;
    private readonly IDbConnection _conn;

    public TodoRepository(ILogger<TodoRepository> logger, IDbConnection conn)
    {
        _logger = logger;
        _conn = conn;
    }

    // GET all todos
    public IEnumerable<TodoItem> GetTodos()
    {
        string sql = "SELECT `Id`, `Name`, `IsComplete`,`Time`,`Priority` FROM `ToDoItem`;";
        return _conn.Query<TodoItem>(sql);
    }

    // GET a single todo by ID
    public TodoItem GetTodo(int id)
    {
        
            string sql = "SELECT `Id`, `Name`, `IsComplete`, `Time`, `Priority` FROM `ToDoItem` WHERE `Id` = @Id;";
            return _conn.QuerySingleOrDefault<TodoItem>(sql, new { Id = id });
    }



    // POST (add) a new todo
    public TodoItem AddTodo(TodoItem todoItem)
    {
        string sql = "INSERT INTO `ToDoItem` (`Name`, `IsComplete`,`Priority`,`Time`) VALUES (@Name, @IsComplete, @Time, @Priority); SELECT LAST_INSERT_ID();";
        var newId = _conn.ExecuteScalar<int>(sql, todoItem);

        if (newId == 0)
            throw new ArgumentException($"Error AddTodo {todoItem}");

        return todoItem with { Id = newId };
    }

    // PUT (update) a todo item
    public int UpdateTodo(TodoItem todoItem)
    {
        string sql = "UPDATE `ToDoItem` SET `Name` = @Name, `IsComplete` = @IsComplete, `Time` = @Time, `Priority`= @Priority WHERE `Id` = @Id;";
        var affectedRows = _conn.Execute(sql, todoItem);

        if (affectedRows == 0)
        {
            _logger.LogWarning($"UpdateTodo not updated {todoItem}");
        }

        return affectedRows;
    }

    // DELETE a todo item
    public void DeleteTodo(TodoItem todoItem)
    {
        string sql = "DELETE FROM `ToDoItem` WHERE `Id` = @Id;";
        _conn.Execute(sql, new { @Id = todoItem.Id });
    }
}
