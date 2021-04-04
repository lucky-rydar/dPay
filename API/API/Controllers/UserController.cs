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
    }
}
