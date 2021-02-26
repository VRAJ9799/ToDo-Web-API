using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo_Application.Models;

namespace ToDo_Application.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private ApplicationDbContext _context;
        public TodosController(ApplicationDbContext context)
        {
            _context = context;
        }

        //[Route("GetAllTodos")]
        //GET /api/todos
        [HttpGet]
        public async Task<ActionResult<List<ToDo>>> GetAllTodos()
        {
            return Ok(await _context.ToDos.ToListAsync());
        }
        //GET /api/todos/id
        [Route("{id:long}")]
        [HttpGet]
        public async Task<ActionResult<ToDo>> GetToDoItem(long id)
        {
            ToDo todo = await _context.ToDos.FindAsync(id);
            if (todo == null)
                return NotFound("There is no item with this id");
            return Ok(todo);
        }
        //POST /api/todos
        [HttpPost]
        public async Task<ActionResult> AddToDo([FromBody] ToDo todo)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            _context.Add(todo);
            await _context.SaveChangesAsync();
            //return Ok();
            return CreatedAtAction(nameof(GetToDoItem), new { id = todo.Id }, todo);
        }
        //PUT /api/todos/id
        [HttpPut]
        [Route("{id:long}")]
        public async Task<ActionResult> UpdateTodo(long id, [FromBody] ToDo todo)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            ToDo todo1 = await _context.ToDos.FindAsync(id);
            if (todo1 != null)
            {
                todo1.Name = todo.Name;
                todo1.IsCompleted = todo.IsCompleted;
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
        //DELETE /api/todos/id
        [HttpDelete]
        [Route("{id:long}")]
        public async Task<ActionResult> DeleteTodo(long id)
        {
            ToDo todo = await _context.ToDos.FindAsync(id);
            if (todo == null)
                return NotFound("There is no item with this id");
            _context.ToDos.Remove(todo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
