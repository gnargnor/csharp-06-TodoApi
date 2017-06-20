//
using System.Collections.Generic;
//allows us to query data within the program
using System.Linq;
//specifies that we are using the MVC model
using Microsoft.AspNetCore.Mvc;
//allows this controller to use the TodoApi Models folder and its contents
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem() { Name = "Item1" });
                _context.SaveChanges();
            }
        }
    }
}
