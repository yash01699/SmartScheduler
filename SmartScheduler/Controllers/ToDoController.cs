using Microsoft.AspNetCore.Mvc;
using SmartScheduler.Models;
using SmartScheduler.Services;

namespace SmartScheduler.Controllers;


[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private readonly ITodoService _service;

    public TodoController(ILogger<TodoController> logger, ITodoService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public IEnumerable<TodoItem> Get()
    {
        return _service.GetTodos();
    }

    [HttpGet("{id}")]
    [Route("getById")]
    public ActionResult<TodoItem> Get([FromQuery]int id)
    {
        var item = _service.GetTodo(id);

        if (item == null)
        {
            _logger.LogError("ID:" + id + " not found");
            return NotFound("Couldnt find an entry with that ID");
        }

        _logger.LogInformation("ID"+id+" returned");
        return item;
    }

    [HttpPost]
    public ActionResult<TodoItem> Post(TodoItem todoItem)
    {
        var newItem = _service.AddTodo(todoItem);

        CreatedAtAction("Get", new { Id = newItem.Id }, newItem);

        return Ok("Created new to do item succesfully!");
    }

    [HttpPut]
    public ActionResult<TodoItem> Put(TodoItem todoItem)
    {
        var todoItem1 = _service.GetTodo(todoItem.Id);

        if (todoItem1 == null)
        {
            return NotFound("Couldnt find a task with the ID");
        }
        else
        {
            var affectedRows = _service.UpdateTodo(todoItem);
            if (affectedRows == 0)
                return NotFound();
        }

        return Ok("Updated succesfully!");
    }

    [HttpDelete]
    public ActionResult<TodoItem> Delete(int id)
    {
        var todoItem = _service.GetTodo(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        _service.DeleteTodo(todoItem);
        return Ok("Deleted succesfully!");
    }
}