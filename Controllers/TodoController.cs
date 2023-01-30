using DevToDoList.Contexts;
using DevToDoList.Models;
using DevToDoList.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevToDoList.Controllers;

public class TodoController : Controller
{
  private readonly AppDbContext _context;
  private readonly ILogger<TodoController> _logger;

  public TodoController(AppDbContext context, ILogger<TodoController> logger)
  {
    this._context = context;
    this._logger = logger;
  }

  public IActionResult Index()
  {
    var todos = this._context.ToDos.OrderBy(t => t.Date).ToList();
    var listTodos = new ListTodoViewModel { Todos = todos };

    ViewData["Title"] = "Lista de tarefas";
    return View(listTodos);
  }

  public IActionResult Delete(int id)
  {
    var task = this._context.ToDos.Find(id);

    if (task is null)
    {
      return NotFound();
    }

    this._context.ToDos.Remove(task);
    this._context.SaveChanges();

    return RedirectToAction(nameof(Index));

  }

  public IActionResult Create()
  {
    ViewData["Title"] = "Criar Tarefa";
    ViewData["TextButton"] = "Criar tarefa";
    return View("Form");
  }

  [HttpPost]
  public IActionResult Create(FormTodoViewModel data)
  {

    var todo = new ToDo(data.Title, data.Date);

    this._context.ToDos.Add(todo);
    this._context.SaveChanges();

    return RedirectToAction(nameof(Index));
  }

  public IActionResult Edit(int id)
  {
    var todo = this._context.ToDos.Find(id);
    if (todo is null)
    {
      return NotFound();
    }
    ViewData["Title"] = "Editar Tarefa";
    ViewData["TextButton"] = "Editar tarefa";
    var editTodo = new FormTodoViewModel { Title = todo.Title, Date = todo.Date };
    return View("Form", editTodo);
  }

  [HttpPost]
  public IActionResult Edit(int id, FormTodoViewModel data)
  {
    var editTodo = this._context.ToDos.Find(id);

    if (editTodo is null)
    {
      return NotFound();
    }

    editTodo.Title = data.Title;
    editTodo.Date = data.Date;

    this._context.SaveChanges();

    return RedirectToAction(nameof(Index));
  }

  public IActionResult ToComplete(int id)
  {
    var editTodo = this._context.ToDos.Find(id);

    if (editTodo is null)
    {
      return NotFound();
    }

    editTodo.IsCompleted = true;
    this._context.SaveChanges();

    return RedirectToAction(nameof(Index));
  }

}