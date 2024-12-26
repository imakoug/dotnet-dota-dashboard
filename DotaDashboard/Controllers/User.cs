using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DotaDashboard.Models;
using DotaDashboard.Data;
using Microsoft.EntityFrameworkCore;

namespace DotaDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiContext _context;

        public UserController (ApiContext context)
        {
            _context = context;
        }

        [HttpPost]

        public IActionResult Create (User user)
        {
            var userInDb = _context.Users.Find(user.Username, user.SteamId);
            if (userInDb != null)
            {
                return Conflict(new { message = "User already exists", e = "409"});
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "User created!" });
        }

        [HttpGet]

        public IActionResult Get ()
        {
            var users = _context.Users.ToArray();
            if(users.Length == 0)
            {
                return Ok(new { e = "There are no users." });
            }

            return Ok(users);
        }

        [HttpDelete]

        public IActionResult Delete (User user)
        {
            var userInDb = _context.Users.Find(user.Username, user.SteamId);
            if (userInDb == null)
            {
                return NotFound(new { message = "This user doesnt exist", e = 404});
            }
            _context.Users.Remove(userInDb);
            _context.SaveChanges();
            return Ok(new { message = "User deleted!"});
        }
    }
}
