using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    [Route("v1")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        [Route("todos")]
        public List<TodoClass> Get()
        {
            return new List<TodoClass>();
        }
    }
}