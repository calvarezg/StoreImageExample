using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreImageExample.Data;
using StoreImageExample.Model;

namespace StoreImageExample.Controllers
{
    [ApiController]
    [Route("/api/images")]
    public class ImagesController : Controller
    {
        private readonly StoreImageDbContext _context;

        public ImagesController(StoreImageDbContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Images.ToListAsync());
        }

        [HttpGet("img")]
        public IActionResult Get()
        {
            return View("Index");
        }

        
        [HttpGet("{id}")]
        public IActionResult Image(int id)
        {
            var bytes = _context.Images.Find(id).ImageFile;
            var imageAsBase64 = Convert.ToBase64String(bytes);
            var returnVal = $"data:image/jpeg;base64,{imageAsBase64}";
            ViewBag.img = returnVal;
            return View("index");
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile formImage)
        {
            if (ModelState.IsValid)
            {
                byte[] bytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    formImage.CopyTo(ms);
                    bytes = ms.ToArray();
                }
                Image image = new() {
                    FileName = formImage.FileName,
                    ImageFile = bytes
                };

                _context.Images.Add(image);
                await _context.SaveChangesAsync();
                return Created(nameof(Index), image);
            }
            else
                return BadRequest();
        }
    }
}
