using Microsoft.AspNetCore.Mvc;
using System;

namespace SemenCommanderApi.Controllers
{
    [ApiController]
    [Route("backup/{userId}")]
    public class BackupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet("all")]
        public ActionResult<string> GetAllFiles(Guid userId)
        {
            return "Some data";
        }

        [HttpGet("{fileId}")]
        public ActionResult<string> GetFile(Guid userId, Guid fileId)
        {
            return "Some data";
        }

        [HttpPost]
        public IActionResult PostFile(Guid userId)
        {
            return Ok();
        }

    }
}
