using Microsoft.AspNetCore.Mvc;
using SemenCommanderApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SemenCommanderApi.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly SemenCommanderDBContext context;

        public UserController(SemenCommanderDBContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register(Guid userId, string password)
        {
            StreamReader reader = new StreamReader(HttpContext.Request.Body);
            var user = JsonSerializer.Deserialize<UserWe>(await reader.ReadToEndAsync());

            if (user.UserId != Guid.Empty)
            {
                if (!context.Users.Any(u => u.UserId == user.UserId))
                {
                    User newUser = new User() { UserId = user.UserId, Password = user.Password };
                    context.Users.Add(newUser);
                    context.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
