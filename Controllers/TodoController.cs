using DevToDoList.Contexts;
using DevToDoList.Models;
using DevToDoList.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevToDoList.Controllers;

public class TodoController : Controller
{
  private readonly AppDbContext _context;
  private readonly ILogger<HomeController> _logger;

  public TodoController(AppDbContext context, ILogger<HomeController> logger)
  {
    this._context = context;
    this._logger = logger;
  }

  public IActionResult Index()
  {
    var todos = this._context.ToDos.ToList();
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
    return View();
  }

  [HttpPost]
  public IActionResult Create(CreateTodoViewModel data)
  {

    var todo = new ToDo(data.Title, data.Date);

    this._context.ToDos.Add(todo);
    this._context.SaveChanges();

    return RedirectToAction(nameof(Index));
  }

}