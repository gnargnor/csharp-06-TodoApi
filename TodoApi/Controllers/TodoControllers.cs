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

            Console.WriteLine(_context.TodoItems.Count());
            if (_context.TodoItems.Count() == 0)
            {
                //adds an item to the in-memory database if one doesn't exist
                _context.TodoItems.Add(new TodoItem() { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        //gets all of the todolist items - GET /api/todo
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            //returns 200
            return _context.TodoItems.ToList();
        }

        //gets todolist items by id - GET /api/todo/{id} - naming the route allows you to link to it in an HTTP response
        [HttpGet("{id", Name = "GetTodo")]
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
    }
}
