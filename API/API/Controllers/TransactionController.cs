using API.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        CustomDBContext db;

        public TransactionController(CustomDBContext db)
        {
            this.db = db;
            this.db.Database.EnsureCreated();
        }

        [HttpGet("test")]
        public Transaction Test()
        {
            return new Transaction()
            {
                Id = 0,
                DateTime = "03.04.2021 13:36",
                FromUser = "User1",
                ToUser = "User2",
                FromCard = "1234********4321",
                ToCard = "4321********1234",
                Currency = "USD",
                Amount = 1000
            };
        }
    }
}
