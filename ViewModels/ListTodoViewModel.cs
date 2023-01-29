using DevToDoList.Models;

namespace DevToDoList.ViewModels;

public class ListTodoViewModel
{
  public ICollection<ToDo> Todos { get; set; } = new List<ToDo>();
}