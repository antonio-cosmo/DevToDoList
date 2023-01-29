namespace DevToDoList.Models;
public class ToDo
{
  public int Id { get; set; }
  public string Title { get; set; }
  public DateTime Date { get; set; }
  public bool IsCompleted { get; set; }

  public ToDo(int id, string title, DateTime date, bool isCompleted = false)
  {
    Id = id;
    Title = title;
    Date = date;
    IsCompleted = isCompleted;
  }

  public ToDo(string title, DateTime date, bool isCompleted = false)
  {
    Title = title;
    Date = date;
    IsCompleted = isCompleted;
  }

}