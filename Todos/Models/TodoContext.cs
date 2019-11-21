using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Todos.Models
{
  public class TodoContext : DbContext
  {
    public TodoContext(DbContextOptions<TodoContext> options) : base(options)
    { }

    public DbSet<TodoItem> TodoItems { get; set; }
  }
}