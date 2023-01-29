using DevToDoList.Models;
using Microsoft.EntityFrameworkCore;

namespace DevToDoList.Contexts;

public class AppDbContext : DbContext
{
  public DbSet<ToDo> ToDos => Set<ToDo>();

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseNpgsql(@"Host=localhost:5432;Username=devTodo;Password=devTodo;Database=db_todolist");
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<ToDo>(model =>
    {
      model.HasKey(m => m.Id);
      model.Property(m => m.Id).HasColumnName("id");

      model.Property(m => m.Title)
            .HasColumnName("title")
            .HasColumnType("varchar(255)")
            .IsRequired();

      model.Property(m => m.Date).HasColumnName("date").HasColumnType("date").IsRequired();

      model.Property(m => m.IsCompleted).HasColumnName("is_completed").HasColumnType("boolean").IsRequired();



    });
  }
}