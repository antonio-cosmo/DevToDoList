using System.Diagnostics;
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
    var tasks = this._context.ToDos.OrderBy(t => t.Date).ToList();
    var listTasks = new ListTodoViewModel { Todos = tasks };

    ViewData["Title"] = "Lista de tarefas";
    return View(listTasks);
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

    var task = new ToDo(data.Title, data.Date);

    this._context.ToDos.Add(task);
    this._context.SaveChanges();

    return RedirectToAction(nameof(Index));
  }

  public IActionResult Edit(int id)
  {
    var task = this._context.ToDos.Find(id);
    if (task is null)
    {
      return NotFound();
    }

    var editTask = new FormTodoViewModel { Title = task.Title, Date = task.Date };

    ViewData["Title"] = "Editar Tarefa";
    ViewData["TextButton"] = "Editar tarefa";

    return View("Form", editTask);
  }

  [HttpPost]
  public IActionResult Edit(int id, FormTodoViewModel data)
  {
    var editTask = this._context.ToDos.Find(id);

    if (editTask is null)
    {
      return NotFound();
    }

    editTask.Title = data.Title;
    editTask.Date = data.Date;

    this._context.SaveChanges();

    return RedirectToAction(nameof(Index));
  }

  public IActionResult ToComplete(int id)
  {
    var editTask = this._context.ToDos.Find(id);

    if (editTask is null)
    {
      return NotFound();
    }

    editTask.IsCompleted = true;
    this._context.SaveChanges();

    return RedirectToAction(nameof(Index));
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}