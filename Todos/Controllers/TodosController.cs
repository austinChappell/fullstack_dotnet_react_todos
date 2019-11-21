using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Todos.Models;

namespace Todos.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TodosController : ControllerBase
  {
    private static List<TodoItem> Todos = new List<TodoItem> { };

    [HttpGet]
    public ActionResult<List<TodoItem>> Get()
    {
      return Ok(Todos);
    }

    [HttpPost]
    public ActionResult Post(TodoItem todoItem)
    {
      var existingItem = Todos.Find(item =>
        item.Task.Equals(todoItem.Task, StringComparison.InvariantCultureIgnoreCase));

      if (existingItem != null)
      {
        return BadRequest("Todo already exists.");
      }
      else
      {
        Todos.Add(todoItem);
        var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeUriString(todoItem.Task));
        return Created(resourceUrl, todoItem);
      }
    }

    [HttpPut]
    public ActionResult Put(TodoItem todoItem)
    {
      var existingItem = Todos.Find(item =>
        item.Task.Equals(todoItem.Task, StringComparison.InvariantCultureIgnoreCase));

      if (existingItem == null)
      {
        return BadRequest("Todo not found.");
      }
      else
      {
        existingItem.Done = todoItem.Done;
        return Ok(existingItem);
      }
    }

    [HttpDelete]
    [Route("{task}")]
    public ActionResult Delete(string task)
    {
      var existingItem = Todos.Find(item =>
        item.Task.Equals(task, StringComparison.InvariantCultureIgnoreCase));

      if (existingItem == null)
      {
        return NotFound();
      }

      Todos.Remove(existingItem);
      return NoContent();
    }
  }
}