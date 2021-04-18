using API.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

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

        [HttpGet("register/{username}/{email}/{phone}/{password}")]
        public RegisterStatus Register(string username, string email, string phone, string password)
        {
            string hashedPassword = Hasher.StringToSHA256(password);
            
            if(db.Users.Where(u=>u.Username == username || u.Email == email).ToList().Count == 0)
            {
                // so we can register this user
                string token = "";
                do
                {
                    token = TokenGenerator.Generate();
                } while (db.Users.Where(u => u.Token == token).Count() != 0);
                
                db.Users.Add(new User() {
                    Username = username,
                    Email = email,
                    Phone = phone,
                    Password = hashedPassword,
                    Token = token
                });
                db.SaveChanges();

                return new RegisterStatus() { Registered = true };
            }
            else
            {
                // we cant register new user
                return new RegisterStatus() { Registered = false };
            }
        }

        [HttpGet("login/{username}/{password}")]
        public LoginStatus Login(string username, string password)
        {
            var users = db.Users;

            string hashedPassword = Hasher.StringToSHA256(password);

            var request = users.Where(u => u.Username == username && u.Password == hashedPassword);

            if (request.ToList().Count == 1)
            {
                var user = request.First();

                return new LoginStatus()
                {
                    Logined = true,
                    Token = user.Token,
                    Username = user.Username,
                    Email = user.Email,
                    Phone = user.Phone
                };
            }
            else
            {
                return new LoginStatus()
                {
                    Logined = false
                };
            }
        }
    }
}
