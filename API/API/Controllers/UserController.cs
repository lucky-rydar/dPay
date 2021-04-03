using API.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        CustomDBContext db;

        public UserController(CustomDBContext db)
        {
            this.db = db;
            this.db.Database.EnsureCreated();
        }

        [HttpGet("test")]
        public User Test()
        {
            return new User() { Id = 0, Email = "hello@gmail.com", Phone = "380123456789", Username = "hello", Password = "", Token = "0"};
        }
    }
}
