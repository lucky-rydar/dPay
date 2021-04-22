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
        public Dictionary<string, dynamic> Register(string username, string email, string phone, string password)
        {
            string hashedPassword = Hasher.StringToSHA256(password);

            if (db.Users.Where(u => u.Username == username || u.Email == email).ToList().Count == 0)
            {
                // so we can register this user
                string token = "";
                do
                {
                    token = TokenGenerator.Generate();
                } while (db.Users.Where(u => u.Token == token).Count() != 0);

                db.Users.Add(new User()
                {
                    Username = username,
                    Email = email,
                    Phone = phone,
                    Password = hashedPassword,
                    Token = token
                });
                db.SaveChanges();

                return new Dictionary<string, dynamic>() { { "registered", true } };
            }
            else
            {
                // we cant register new user
                return new Dictionary<string, dynamic>() { { "registered", false } };
            }
        }

        [HttpGet("login/{username}/{password}")]
        public Dictionary<string, dynamic> Login(string username, string password)
        {
            var users = db.Users;

            string hashedPassword = Hasher.StringToSHA256(password);

            var request = users.Where(u => u.Username == username && u.Password == hashedPassword);

            if (request.ToList().Count == 1)
            {
                var user = request.First();

                return new Dictionary<string, dynamic>()
                {
                    { "logined", true},
                    { "token", user.Token},
                    {"username", user.Username },
                    {"email", user.Email },
                    {"phone", user.Phone }
                };

            }
            else
            {
                return new Dictionary<string, dynamic>()
                {
                    { "logined", false},
                    { "token", null},
                    {"username", null},
                    {"email", null},
                    {"phone", null }
                };
            }
        }

        [HttpGet("change_phone/{token}/{new_phone}")]
        public Dictionary<string, dynamic> ChangePhone(string token, string new_phone)
        {
            try
            {
                var user = db.Users.Where(u => u.Token == token).FirstOrDefault();
                user.Phone = new_phone;
                return new Dictionary<string, dynamic>() { { "changed", true } };
            }
            catch(Exception)
            {
                return new Dictionary<string, dynamic>() { { "changed", false } };
            }
        }
    }
}
