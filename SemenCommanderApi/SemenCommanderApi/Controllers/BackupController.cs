using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SemenCommanderApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using File = SemenCommanderApi.Models.File;

namespace SemenCommanderApi.Controllers
{
    [ApiController]
    [Route("backup/{userId}")]
    public class BackupController : Controller
    {
        private readonly SemenCommanderDBContext context;
        public BackupController(SemenCommanderDBContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet("all")]
        public ActionResult<BackupFileWe[]> GetAllFiles(Guid userId)
        {
            BackupFileWe[] files = context.Files.Where(f => f.UserId == userId).Select(f => new BackupFileWe(f)).ToArray();
            return files;
        }

        [HttpGet("{fileId}")]
        public ActionResult<string> GetFile(Guid userId, Guid fileId)
        {
            var file = context.Files.FirstOrDefault(x => x.FileId == fileId && x.UserId == userId);
            if (file != null)
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

                var fileWe = new FileWe(file.FileName, file.FileContent);
                var jsonString = JsonSerializer.Serialize(fileWe);
                return Ok(jsonString);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult PostFile(Guid userId)
        {
            var file = Request.Form.Files.FirstOrDefault();
            try
            {
                using (var stream = file.OpenReadStream())
                {
                    byte[] data = new byte[file.Length];
                    stream.Read(data);
                    var newFile = new File(data, file.FileName, userId);
                    context.Files.Add(newFile);
                    context.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [HttpDelete("{fileId}")]
        public IActionResult DeleteFile(Guid userId, Guid fileId)
        {
            var file = context.Files.FirstOrDefault(x => x.FileId == fileId && x.UserId == userId);
            if (file != null)
            {
                context.Files.Remove(file);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

    }
}
