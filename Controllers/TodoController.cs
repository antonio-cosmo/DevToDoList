using DevToDoList.Contexts;
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
}