using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;
using Todo.ViewModels;

namespace Todo.Controllers
{
    [ApiController]
    [Route("v1")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        [Route("todos")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context
        )
        {
            var todos = await context
                .Todos
                .AsNoTracking()
                .ToListAsync();

            return Ok(todos);
        }

        [HttpGet]
        [Route("todos/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id
        )
        {
            var todo = await context
                .Todos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null) return NotFound();

            return Ok(todo);
        }

        [HttpPost("todos")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateTodoViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var todo = new TodoModel
                {
                    Date = DateTime.Now,
                    Done = false,
                    Title = model.Title
                };

                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();

                return Created($"v1/todos/{todo.Id}", todo);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}