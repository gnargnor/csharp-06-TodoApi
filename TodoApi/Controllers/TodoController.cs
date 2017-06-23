//
using System;
using System.Collections.Generic;
//allows us to query data within the program
using System.Linq;
//specifies that we are using the MVC model
using Microsoft.AspNetCore.Mvc;
//allows this controller to use the TodoApi Models folder and its contents
using TodoApi.Models;

namespace TodoApi.Controllers
{
    //Route(template string of HttpGet Route) - template string is the controller class name minus the Controller suffix - "Todo" in this case
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;
        //uses dependency injection to inject TodoContext into the controller - used in CRUD methods in the controller
        public TodoController(TodoContext context)
        {
            _context = context;


            if (_context.TodoItems.Count() == 0)
            {
                //adds an item to the in-memory database if one doesn't exist
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        //gets all of the todolist items - GET /api/todo
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            //response serialized to JSON -  returned in the body of this response returns 200
            return _context.TodoItems.ToList();
        }

        //gets todolist items by id - GET /api/todo/{id} - naming the route allows you to link to it in an HTTP response
        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            
            var item = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                //built into asp.netcore mvc - returns 404 (not found)
                return NotFound();
            }
            //returns 200
            return new ObjectResult(item);
        }

        //post route
        [HttpPost]
        //get the TodoItem from the body of the request
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _context.TodoItems.Add(item);
            _context.SaveChanges();

            //returns a 201 response (created new resource on server) - also adds a location header that specifies the uri of the newly created todoitem
            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id")]
        public IActionResult Update(long id, [FromBody] TodoItem item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var todo = _context.TodoItems.FirstOrDefault(t => item.Id == id);
            if(todo == null)
            {
                return NotFound();
            }


            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.TodoItems.Update(todo);
            _context.SaveChanges();
            return new NoContentResult();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoItems.First(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }

    }
}
