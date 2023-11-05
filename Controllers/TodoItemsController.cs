using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems([FromQuery] int? status)
        {
            if (_context.TodoItems == null)
            {
            return NotFound();
            }

            IQueryable<TodoItem> query = _context.TodoItems;

            // クエリパラメータがtrueの場合は未完了のTodoのみをフィルタリング
            if (status.HasValue)
            {
                query = query.Where(t => (int)t.Status == status.Value);
            }

            return await query.ToListAsync();
            }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
          if (_context.TodoItems == null)
          {
              return NotFound();
          }
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
        private readonly ILogger<TodoItemsController> _logger;
        public TodoItemsController(TodoContext context, ILogger<TodoItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=212375
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {

            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "データベースの更新中に競合エラーが発生しました。");
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        // PUT: api/TodoItems/{id}/markComplete
        [HttpPut("{id}/markComplete")]
        public async Task<IActionResult> MarkTodoItemAsComplete(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if(todoItem == null)
            {
                return NotFound();
            }

            todoItem.Status = TodoStatus.Complete;

            _context.Entry(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // PUT: api/TodoItems/{id}/markIncomplete
        [HttpPut("{id}/markIncomplete")]
        public async Task<IActionResult> MarkTodoItemAsIncomplete(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if(todoItem == null)
            {
                return NotFound();
            }

            todoItem.Status = TodoStatus.Incomplete; // Status を 0 に設定する

            _context.Entry(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();

            }



        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
        //   if (_context.TodoItems == null)
        //   {
        //       return Problem("Entity set 'TodoContext.TodoItems'  is null.");
        //   }
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            if (_context.TodoItems == null)
            {
                return NotFound();
            }
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return (_context.TodoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
