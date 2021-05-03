using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MvcApp.Data;
using System.Linq;

namespace MvcApp.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TodosController : ControllerBase
	{
		private readonly TodoStore _store;

		public TodosController(TodoStore store) 
			=> _store = store;

		[HttpGet]
		public IActionResult GetAll()
		{
			var todos = _store.GetAll();
			if (!todos.Any())
			{
				return NoContent();
			}

			return Ok(todos);
		}

		[HttpGet("{id:int}")]
		public IActionResult GetOne(int id)
		{
			var todo = _store.GetOne(id);
			if (todo == null)
			{
				return NotFound();
			}

			return Ok(todo);
		}

		[HttpPost]
		public IActionResult Post([FromBody] Todo todo)
		{
			if (todo == null)
			{
				return BadRequest();
			}

			_store.Insert(todo);

			return Created($"{Request.GetDisplayUrl()}/{_store.Counter}", null);
		}

		[HttpPut("{id:int}")]
		public IActionResult Put(int id, [FromBody] Todo todo)
		{
			if (todo == null)
			{
				return BadRequest();
			}

			_store.Upsert(id, todo);

			return Ok(todo);
		}

		[HttpDelete("{id:int}")]
		public IActionResult Delete(int id)
		{
			_store.Delete(id);

			return Ok();
		}
	}
}
