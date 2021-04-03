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
    public class CardController : ControllerBase
    {
        CustomDBContext db;

        public CardController(CustomDBContext db)
        {
            this.db = db;
            this.db.Database.EnsureCreated();
        }

        [HttpGet("test")]
        public Card Test()
        {
            return new Card() { Id = 0, OwnerId = 0, CardNumber = "1234000000001234", CVV = "111", ExpMonth = "11", ExpYear = "30", IsDefault = false, Name = "First Card"};
        }
    }
}
